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
            object obj = CreatNodeData("ET.RootNodeData");
            
            NodeHelper.SetField(obj, "Child", null);
        }

        public static object CreatNodeData(string nodeName)
        {
            if (!nodeName.StartsWith("ET.")) nodeName = "ET." + nodeName;
            if (!nodeName.EndsWith("NodeData")) nodeName += "NodeData";

            object obj = Model.CreateInstance(nodeName);
            
            return obj;
        }

        public static object CreatNodeData(string nodeName, params object[] args)
        {
            if (!nodeName.StartsWith("ET.")) nodeName = "ET." + nodeName;
            if (!nodeName.EndsWith("NodeData")) nodeName += "NodeData";

            object obj = Model.CreateInstance(nodeName, true, BindingFlags.Public, null, args, null, null);

            return obj;
        }

        public static void SetField(object obj, string name, object value)
        {
            Type type = obj.GetType();
            while (type != typeof(object))
            {
                var info = type.GetField(name);
                if (info != null)
                {
                    info.SetValue(obj, value);
                    return;
                }
                else type = type.BaseType;
            }
            
            Debug.LogError($"未找到{type}中字段{name}");
        }

        public static void SetField(object obj, params (string, object)[] fieldTuples)
        {
            foreach (var field in fieldTuples)
            {
                SetField(obj, field.Item1, field.Item2);
            }
        }
    }
}