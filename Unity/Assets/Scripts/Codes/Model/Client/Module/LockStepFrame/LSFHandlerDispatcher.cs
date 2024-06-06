

using System;
using System.Collections.Generic;

namespace ET.Client
{
    public static class LSFHandlerDispatcher
    {
        [StaticField]
        public static Dictionary<Type, ILSFHandler> Handler = new();
        
        static LSFHandlerDispatcher()
        {
            Handler?.Clear();
            
            var handlerTypes = EventSystem.Instance.GetTypes(typeof (LSFHandlerAttribute));
            foreach (Type type in handlerTypes)
            {
                ILSFHandler handler = Activator.CreateInstance(type) as ILSFHandler;
                if (handler == null)
                {
                    Log.Error($"Can not find Handler: {type.Name}");
                    continue;
                }

                LSFHandlerAttribute attribute = type.GetCustomAttributes(typeof (LSFHandlerAttribute), false)[0] as LSFHandlerAttribute;
                
                Handler.Add(attribute.ComponentType, handler);
                Handler.Add(attribute.CmdType, handler);
            }
        }
    }
}