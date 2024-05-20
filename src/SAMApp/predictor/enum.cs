using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMApp
{
    public static class MyEnum
    {
        public static T GetEngineType<T>(string strType)
        {
            T t = (T)EngineType.Parse(typeof(T), strType);
            return t;
        }
    }
    public enum EngineType
    {
        OpenVINO,
        TensorRT,
        ONNX,
        NULL
    }

}
