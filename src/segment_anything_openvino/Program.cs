using OpenCvSharp;
using OpenVinoSharp.Extensions.process;
using OpenVinoSharp.Extensions;
using OpenVinoSharp;
using System.Diagnostics;
using System.Reflection.Metadata.Ecma335;
using System;

namespace segment_anything_openvino
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string embedding_model = "./../../../../../model/vit_b_encoder/vit_b_encoder.onnx";
            string decoding_model = "./../../../../../model/vit_b_decoder.onnx";
            string image_path = "./../../../../../images/dog.jpg";
            string image_embedding_path = "./../../../../../images/dog.bin";

            Mat img = Cv2.ImRead(image_path);
            float factor = 0;
            Resize.letterbox_img(img, 1024, out factor);
            if (!File.Exists(image_embedding_path)) 
            {
                float[] data = ImageEmbeddings(img, embedding_model);
                SaveToFile(data, image_embedding_path);
            }


            float[] image_embedding_data = LoadFromFile(image_embedding_path);
            float[] onnx_coord = new float[6] { 600f / factor, 200f / factor, 480 / factor, 130 / factor, (480 + 190)/factor, (130 + 140)/factor };
            float[] onnx_label = new float[3] { 1f, 2f, 3f };
            float[] onnx_mask_input = new float[256 * 256];
            float[] onnx_has_mask_input = new float[1] { 0 };
            float[] img_size = new float[2] { img.Height, img.Width };
            byte[] result = ImageDecodings(decoding_model, image_embedding_data, onnx_coord, onnx_label, onnx_mask_input, onnx_has_mask_input, img_size);


            Cv2.Rectangle(img, new Rect(600, 200, 20, 20), new Scalar(0, 0, 255), -1);
            Cv2.Rectangle(img, new Rect(480, 130, 190, 140), new Scalar(0, 255, 255), 2);
            Mat mask = new Mat(img.Rows, img.Cols, MatType.CV_8UC1, result);
            Mat rgb_mask = Mat.Zeros(new Size(img.Cols, img.Rows), MatType.CV_8UC3);
            Cv2.Add(rgb_mask, new Scalar(255.0, 144.0, 37.0, 0.6), rgb_mask, mask);
            Mat new_mat = new Mat();
            Cv2.AddWeighted(img, 0.5, rgb_mask, 0.5, 0.0, new_mat);
            Cv2.ImShow("mask", new_mat);
            Cv2.WaitKey(0);
        }


        static byte[] ImageDecodings(string model_path, float[] image_embeddings, float[] onnx_coord, 
            float[] onnx_label, float[] onnx_mask_input, float[] onnx_has_mask_input, float[] img_size) 
        {
            Core core = new Core();
            Model model = core.read_model(model_path);
            OvExtensions.printf_model_info(model);
            CompiledModel compiled = core.compile_model(model, "CPU");
            Console.WriteLine("Compile Model Sucessfully!");
            InferRequest request = compiled.create_infer_request();
            Tensor tensor1 = request.get_tensor("image_embeddings");
            tensor1.set_data(image_embeddings);
            Tensor tensor2 = request.get_tensor("point_coords");
            tensor2.set_shape(new Shape(1, 3, 2));
            tensor2.set_data(onnx_coord);
            Tensor tensor3 = request.get_tensor("point_labels");
            tensor3.set_shape(new Shape(1, 3));
            tensor3.set_data(onnx_label);
            Tensor tensor4 = request.get_tensor("mask_input");
            tensor4.set_data(onnx_mask_input);
            Tensor tensor5 = request.get_tensor("has_mask_input");
            tensor5.set_data(onnx_has_mask_input);
            Tensor tensor6 = request.get_tensor("orig_im_size");
            tensor6.set_data(img_size);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            request.infer();
            sw.Stop();
            Console.WriteLine("Inference time: " + sw.ElapsedMilliseconds);
            Tensor output_tensor = request.get_tensor("masks");
            float[] mask_data = output_tensor.get_data<float>((int)output_tensor.get_size());
            byte[] mask_data_byte = new byte[mask_data.Length];
            for (int i = 0; i < mask_data.Length; i++)
            {
                mask_data_byte[i] = (byte)(mask_data[i] > 0 ? 255 : 0);
            }
            return mask_data_byte;
        }

        static float[] ImageEmbeddings(Mat img, string model_path)
        {
            Core core = new Core();
            Model model = core.read_model(model_path); 
            OvExtensions.printf_model_info(model);
            CompiledModel compiled = core.compile_model(model, "CPU");
            Console.WriteLine("Compile Model Sucessfully!");
            InferRequest request = compiled.create_infer_request();
            Mat mat = new Mat();
            Cv2.CvtColor(img, mat, ColorConversionCodes.BGR2RGB);
            float factor = 0;
            mat = Resize.letterbox_img(mat, 1024, out factor);
            mat = Normalize.run(mat, new float[] { 123.675f, 116.28f, 103.53f }, new float[] { 1.0f / 58.395f, 1.0f / 57.12f, 1.0f / 57.375f }, false);
            Tensor input_tensor = request.get_input_tensor();
            float[] input_data = Permute.run(mat);
            input_tensor.set_data(input_data);
            Stopwatch sw = new Stopwatch();
            sw.Start();
            request.infer();
            sw.Stop();
            Console.WriteLine("Inference time: " + sw.ElapsedMilliseconds);
            Tensor output_tensor = request.get_output_tensor();
            Console.WriteLine(output_tensor.get_shape().to_string());
            return output_tensor.get_data<float>((int)output_tensor.get_size());
        }


        static void SaveToFile(float[] array, string filePath)
        {
            using (FileStream fs = new FileStream(filePath, FileMode.Create))
            using (BinaryWriter bw = new BinaryWriter(fs))
            {
                foreach (float value in array)
                {
                    bw.Write(value);
                }
            }
        }

        static float[] LoadFromFile(string filePath)
        {
            float[] loadedArray;
            using (FileStream fs = new FileStream(filePath, FileMode.Open))
            using (BinaryReader br = new BinaryReader(fs))
            {
                loadedArray = new float[fs.Length / sizeof(float)];
                for (int i = 0; i < loadedArray.Length; i++)
                {
                    loadedArray[i] = br.ReadSingle();
                }
            }
            return loadedArray;
        }

    }
}
