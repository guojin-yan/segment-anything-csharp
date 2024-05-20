using OpenCvSharp;
using OpenVinoSharp.Extensions.process;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace SAMApp
{
    public class EncodePredictor :IDisposable
    {
        private Predictor m_predictor;
        public EncodePredictor(string model_path, EngineType engine, string device)
        {
            m_predictor = new Predictor(model_path, engine, device);
        }

        public void Dispose()
        {
            m_predictor.Dispose();
        }

        public float[] infer(Mat img) 
        {
            Mat mat = new Mat();
            Cv2.CvtColor(img, mat, ColorConversionCodes.BGR2RGB);
            float factor = 0;
            mat = Resize.letterbox_img(mat, 1024, out factor);
            mat = Normalize.run(mat, new float[] { 123.675f, 116.28f, 103.53f }, new float[] { 1.0f / 58.395f, 1.0f / 57.12f, 1.0f / 57.375f }, false);
            float[] input_data = Permute.run(mat);
            return m_predictor.infer(new List<float[]> { input_data }, new List<string> { "images" }, new List<int[]>{ new int[] { 1, 3, 1024, 1024 }}, new List<string> { "embeddings" }, new List<int[]> { new int[] { 1, 256, 64*64 } })[0];
        }
    }

}
