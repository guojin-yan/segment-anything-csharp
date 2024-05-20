using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMApp
{
    public class DecoderPredictor : IDisposable
    {

        private Predictor m_predictor;
        private List<string> m_input_names;
        private List<int[]> m_input_sizes;
        private List<string> m_output_names;

        public DecoderPredictor(string model_path, EngineType engine, string device)
        {
            m_predictor = new Predictor(model_path, engine, device);
            m_input_names = new List<string> { "image_embeddings", "point_coords", "point_labels", "mask_input", "has_mask_input", "orig_im_size" };
            m_input_sizes = new List<int[]> { new int[] { 1, 256, 64, 64 }, new int[] { 1, 2, 2 }, new int[] { 1, 2 }, new int[] { 1, 1, 256, 256 }, new int[] { 1 }, new int[] { 2 } };
            m_output_names = new List<string> { "mask" };
        }

        public Mat infer(float[] image_embeddings,float[] point_coords, float[] point_labels, float[] mask_input,float[] has_mask_input, float[] orig_im_size) 
        {
            int n = point_coords.Length / 2;
            m_input_sizes[1][1] = n;
            m_input_sizes[2][1] = n;
            List<float[]> results =  m_predictor.infer(new List<float[]> { image_embeddings, point_coords, point_labels, mask_input, has_mask_input, orig_im_size },
                new List<string> { "image_embeddings", "point_coords", "point_labels", "mask_input", "has_mask_input", "orig_im_size" },
                m_input_sizes, m_output_names, new List<int[]>());
            byte[] mask_data_byte = new byte[results[0].Length];
            for (int i = 0; i < results[0].Length; i++)
            {
                mask_data_byte[i] = (byte)(results[0][i] > 0 ? 255 : 0);                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                          
            }
            return new Mat((int)orig_im_size[0], (int)orig_im_size[1], MatType.CV_8UC1, mask_data_byte);

        }
        public void Dispose()
        {
            m_predictor.Dispose();
        }
    }
}
