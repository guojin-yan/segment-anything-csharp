using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using OpenCvSharp;
using OpenCvSharp.Dnn;
using OpenCvSharp.ML;
using OpenVinoSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Size = OpenCvSharp.Size;

namespace SAMApp
{
    public class Predictor :IDisposable
    {
        private Core core;
        private Model model;
        private CompiledModel compiled;
        private InferRequest openvino_infer;
        //private Nvinfer tensorrt_infer;
        private SessionOptions options;
        private InferenceSession onnx_infer;
        private EngineType engine = EngineType.NULL;

        public Predictor() { }
        public Predictor(string model_path, EngineType engine, string device) 
        {
            if (model_path == null) 
            {
                throw new ArgumentNullException(nameof(model_path));
            }
            this.engine = engine;
            if (engine == EngineType.OpenVINO)
            {
                core = new Core();
                model = core.read_model(model_path);
                compiled = core.compile_model(model, device);
                openvino_infer = compiled.create_infer_request();
            }
            else if (engine == EngineType.ONNX)
            {
                if (device == "CPU")
                {
                    options = new SessionOptions();
                    options.LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_INFO;
                    options.AppendExecutionProvider_CPU(0);
                    onnx_infer = new InferenceSession(model_path, options);
                }
                else if (device == "GPU.0")
                {

                    options = new SessionOptions();
                    options.LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_INFO;
                    options.AppendExecutionProvider_CUDA(0);
                    onnx_infer = new InferenceSession(model_path, options);
                }
                else if (device == "GPU.1")
                {
                    options = new SessionOptions();
                    options.LogSeverityLevel = OrtLoggingLevel.ORT_LOGGING_LEVEL_INFO;
                    options.AppendExecutionProvider_CUDA(1);
                    onnx_infer = new InferenceSession(model_path, options);
                }
            }
            else if (engine == EngineType.TensorRT) 
            {
                //tensorrt_infer = new Nvinfer(model_path);
            }
        }

        public void Dispose()
        {
            switch (engine)
            {
                case EngineType.TensorRT:
                    {
                        //tensorrt_infer.Dispose();
                        break;
                    }
                case EngineType.OpenVINO:
                    {
                        openvino_infer.Dispose();
                        compiled.Dispose();
                        model.Dispose();
                        core.Dispose();
                        break;
                    }
                case EngineType.ONNX:
                    {
                        onnx_infer.Dispose();
                        options.Dispose();
                        break;
                    }
                case EngineType.NULL:
                    {
                        break;
                    }
            }
            GC.Collect();

        }

        public List<float[]> infer(List< float[] >input_datas, List<string> input_names, List< int[] >input_sizes, List<string> output_names, List<int[]> output_sizes) 
        {

            List<float[]> returns = new List<float[]>();
            switch (engine)
            {
                case EngineType.TensorRT:
                    {
                        //tensorrt_infer.LoadInferenceData(input_names[0], input_data);
                        //tensorrt_infer.infer();
                        //foreach (var name in output_names) 
                        //{
                        //    returns.Add(tensorrt_infer.GetInferenceResult(name));
                        //}
                        break;
                    }
                case EngineType.OpenVINO:
                    {
                        for (int i = 0; i < input_names.Count; ++i) 
                        {
                            var input_tensor = openvino_infer.get_tensor(input_names[i]);
                            input_tensor.set_shape(new Shape(input_sizes[i]));
                            input_tensor.set_data(input_datas[i]);
                        }
                        openvino_infer.infer();
                        foreach (var name in output_names)
                        {
                            var output_tensor = openvino_infer.get_tensor(name);
                            returns.Add(output_tensor.get_data<float>((int)output_tensor.get_size()));
                        }
                        
                        break;
                    }
                case EngineType.ONNX:
                    {
                        var inputs = new Dictionary<string, OrtValue>();
                        for (int i = 0; i < input_names.Count; ++i)
                        {
                            List<long> size = new List<long>();
                            foreach (var s in input_sizes[i]) 
                            {
                                size.Add(s);
                            }
                            var inputOrtValue = OrtValue.CreateTensorValueFromMemory(input_datas[i], size.ToArray());
                            inputs.Add(input_names[i], inputOrtValue);
                        }
                        var runOptions = new RunOptions();

                        // Pass inputs and request the first output
                        // Note that the output is a disposable collection that holds OrtValues
                        var outputs = onnx_infer.Run(runOptions, inputs, onnx_infer.OutputNames);
                        foreach (var output in outputs)
                        {
                            var outputData = output.GetTensorDataAsSpan<float>();
                            float[] data = new float[outputData.Length];
                            output.GetTensorDataAsSpan<float>().CopyTo(data);
                            returns.Add(data);
                        }

                        break;
                    }
                case EngineType.NULL:
                    {
                        break;
                    }
            }
            return returns;
        }
    }
}
