using System;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ET
{

    public static class NodeHelper
    {
        public static Assembly Model { get; private set; }

        static NodeHelper()
        {
            Model = Assembly.Load(File.ReadAllBytes($"{Application.dataPath}/Bundles/Code/Model.dll.bytes"));
        }

        public static void Test()
        {
            foreach (Type type in Model.GetTypes())
            {
                if (type.ToString().EndsWith("NodeData"))
                    Debug.Log(type.ToString());
            }
        }

        public static object CreatNodeData(string nodeName, params (string, object)[] propertyTuples)
        {
            if (!nodeName.StartsWith("ET.")) nodeName = "ET." + nodeName;
            if (!nodeName.EndsWith("NodeData")) nodeName += "NodeData";

            object obj = Model.CreateInstance(nodeName);

            foreach (var property in propertyTuples)
            {
                SetProperty(obj, property.Item1, property.Item2);
            }

            return obj;
        }

        public static object CreatNodeData(string nodeName, object[] args, params (string, object)[] propertyTuples)
        {
            if (!nodeName.EndsWith("NodeData")) nodeName += "NodeData";

            object obj = Model.CreateInstance(nodeName, true, BindingFlags.Public, null, args, null, null);

            foreach (var property in propertyTuples)
            {
                SetProperty(obj, property.Item1, property.Item2);
            }

            return obj;
        }

        public static void SetProperty(object obj, string name, object value)
        {
            obj.GetType().GetProperty(name).SetValue(obj, value);
        }
    }
}