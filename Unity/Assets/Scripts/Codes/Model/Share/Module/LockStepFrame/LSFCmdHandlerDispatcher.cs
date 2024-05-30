using System;
using System.Collections.Generic;

namespace ET
{
    public static class LSFCmdHandlerDispatcher
    {
        [StaticField]
        public static Dictionary<Type, LSFCmdHandler> Client = new();
        
        [StaticField]
        public static Dictionary<Type, LSFCmdHandler> Server = new();

        static LSFCmdHandlerDispatcher()
        {
            var handlers = EventSystem.Instance.GetTypes(typeof (LSFCmdHandlerAttribute));
            foreach (Type type in handlers)
            {
                LSFCmdHandler handler = Activator.CreateInstance(type) as LSFCmdHandler;
                if (handler == null)
                {
                    Log.Error($"Can not find Handler: {type.Name}");
                    continue;
                }
                    
                LSFCmdHandlerAttribute attribute = type.GetCustomAttributes(typeof (LSFCmdHandlerAttribute), false)[0] as LSFCmdHandlerAttribute;
                if (!attribute.Type.IsSubclassOf(typeof (LSFCmd))) // 检查是否是LSFCmd
                {
                    Log.Error($"Attribute args error: {type.Name}");
                    continue;
                }
                    
                if (attribute.Type.ToString().StartsWith("ET.Client"))
                {
                    LSFCmdHandlerDispatcher.Client.Add(attribute.Type, handler);
                }
                else
                {
                    LSFCmdHandlerDispatcher.Server.Add(attribute.Type, handler);
                }
            }
        }
    }
}