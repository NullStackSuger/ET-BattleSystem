using System;
using System.Collections.Generic;

namespace ET
{
    public static class LSFComponentHandlerDispatcher
    {
        [StaticField]
        public static Dictionary<Type, ET.Client.LSFComponentHandler> Client = new();
        
        [StaticField]
        public static Dictionary<Type, ET.Server.LSFComponentHandler> Server = new();

        static LSFComponentHandlerDispatcher()
        {
            var handlers = EventSystem.Instance.GetTypes(typeof (ET.Client.LSFComponentHandlerAttribute));
            foreach (Type type in handlers)
            {
                ET.Client.LSFComponentHandler handler = Activator.CreateInstance(type) as ET.Client.LSFComponentHandler;
                if (handler == null)
                {
                    Log.Error($"Can not find Handler: {type.Name}");
                    continue;
                }
                    
                ET.Client.LSFComponentHandlerAttribute attribute = type.GetCustomAttributes(typeof (ET.Client.LSFComponentHandlerAttribute), false)[0] as ET.Client.LSFComponentHandlerAttribute;
                if (!attribute.Type.IsSubclassOf(typeof (Entity))) // 检查是否是Component
                {
                    Log.Error($"Attribute args error: {type.Name}");
                    continue;
                }
                    
                LSFComponentHandlerDispatcher.Client.Add(attribute.Type, handler);
            }
            
            handlers = EventSystem.Instance.GetTypes(typeof (ET.Server.LSFComponentHandlerAttribute));
            foreach (Type type in handlers)
            {
                ET.Server.LSFComponentHandler handler = Activator.CreateInstance(type) as ET.Server.LSFComponentHandler;
                if (handler == null)
                {
                    Log.Error($"Can not find Handler: {type.Name}");
                    continue;
                }
                    
                ET.Server.LSFComponentHandlerAttribute attribute = type.GetCustomAttributes(typeof (ET.Server.LSFComponentHandlerAttribute), false)[0] as ET.Server.LSFComponentHandlerAttribute;
                if (!attribute.Type.IsSubclassOf(typeof (Entity))) // 检查是否是Component
                {
                    Log.Error($"Attribute args error: {type.Name}");
                    continue;
                }
                    
                LSFComponentHandlerDispatcher.Server.Add(attribute.Type, handler);
            }
        }
    }
}