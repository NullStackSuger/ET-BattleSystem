using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace ET
{

    public static class ReflectHelper
    {
        public static Assembly Model { get; private set; }

        static ReflectHelper()
        {
            Model = Assembly.Load(File.ReadAllBytes($"{Application.dataPath}/Bundles/Code/Model.dll.bytes"));
        }

        public static List<Type> GetAllCmds(this CreatLSFCmdEditor self)
        {
            List<Type> res = new();
            foreach (Type type in Model.GetTypes())
            {
                if (type.Name.StartsWith("LSF") && type.Name.EndsWith("Cmd") && type.Name != "LSFCmd")
                    res.Add(type);
            }
            return res;
        }
        
        public static List<Type> GetAllNodes(this CreatNodeEditor self)
        {
            List<Type> res = new();
            foreach (Type type in Model.GetTypes())
            {
                if (type.ToString().EndsWith("NodeData"))
                    res.Add(type);
            }
            return res;
        }

        public static object CreatNodeData(this CreatNodeEditor self, string nodeName)
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

        public static object GetField(object obj, string name)
        {
            Type type = obj.GetType();
            while (type != typeof (object))
            {
                var info = type.GetField(name);
                if (info != null) return info.GetValue(obj);
                else type = type.BaseType;
            }
            throw new Exception($"{obj.GetType()}中没有{name}字段");
        }
    }
}