

using System;
using System.Collections.Generic;

namespace ET.Server
{
    public static class LSFHandlerDispatcher
    {
        [StaticField]
        public static Dictionary<Type, ILSFHandler> Handlers = new();
        /// <summary>
        /// ComponentType和CmdType互相查找
        /// </summary>
        // Key: ComponentType Value: CmdType || Key:CmdType, Value:ComponentType
        [StaticField]
        public static Dictionary<Type, Type> Types = new();
        
        static LSFHandlerDispatcher()
        {
            Handlers?.Clear();
            Types?.Clear();
            
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
                
                Handlers.Add(attribute.ComponentType, handler);
                Handlers.Add(attribute.CmdType, handler);
                Types.Add(attribute.ComponentType, attribute.CmdType);
                Types.Add(attribute.CmdType, attribute.ComponentType);
            }
        }
    }
}