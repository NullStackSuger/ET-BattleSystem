using System;

namespace ET.Server
{

    public static class LSFComponentHandlerDispatcherComponentSystem
    {
        public class AwakeSystem : AwakeSystem<LSFComponentHandlerDispatcherComponent>
        {
            protected override void Awake(LSFComponentHandlerDispatcherComponent self)
            {
                LSFComponentHandlerDispatcherComponent.Instance = self;
                // Load
                var handlers = EventSystem.Instance.GetTypes(typeof (LSFComponentHandlerAttribute));
                foreach (Type type in handlers)
                {
                    LSFComponentHandler handler = Activator.CreateInstance(type) as LSFComponentHandler;
                    if (handler == null)
                    {
                        Log.Error($"robot ai is not ANodeHandler: {type.Name}");
                        continue;
                    }
                
                    // 获取NodeHandlerAttribute.NodeType
                    LSFComponentHandlerAttribute attribute = type.GetCustomAttributes(typeof (LSFComponentHandlerAttribute), false)[0] as LSFComponentHandlerAttribute;
                    
                    self.Handlers.Add(attribute.Type,  handler);
                }
            }
        }

        public class DestroySystem: DestroySystem<LSFComponentHandlerDispatcherComponent>
        {
            protected override void Destroy(LSFComponentHandlerDispatcherComponent self)
            {
                LSFComponentHandlerDispatcherComponent.Instance = null;
                self.Handlers.Clear();
                self.Handlers = null;
            }
        }
    }
}