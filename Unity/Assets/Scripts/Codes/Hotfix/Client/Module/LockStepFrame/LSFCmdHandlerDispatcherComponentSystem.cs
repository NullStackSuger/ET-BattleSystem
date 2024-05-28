using System;

namespace ET.Client
{

    public static class LSFCmdHandlerDispatcherComponentSystem
    {
        public class AwakeSystem : AwakeSystem<LSFCmdHandlerDispatcherComponent>
        {
            protected override void Awake(LSFCmdHandlerDispatcherComponent self)
            {
                LSFCmdHandlerDispatcherComponent.Instance = self;
                // Load
                var handlers = EventSystem.Instance.GetTypes(typeof (LSFCmdHandlerAttribute));
                foreach (Type type in handlers)
                {
                    LSFCmdHandler handler = Activator.CreateInstance(type) as LSFCmdHandler;
                    if (handler == null)
                    {
                        Log.Error($"robot ai is not ANodeHandler: {type.Name}");
                        continue;
                    }
                
                    // 获取NodeHandlerAttribute.NodeType
                    LSFCmdHandlerAttribute attribute = type.GetCustomAttributes(typeof (LSFCmdHandlerAttribute), false)[0] as LSFCmdHandlerAttribute;
                    
                    self.Handlers.Add(attribute.Type,  handler);
                }
            }
        }

        public class DestroySystem: DestroySystem<LSFCmdHandlerDispatcherComponent>
        {
            protected override void Destroy(LSFCmdHandlerDispatcherComponent self)
            {
                LSFCmdHandlerDispatcherComponent.Instance = null;
                self.Handlers.Clear();
                self.Handlers = null;
            }
        }
    }
}