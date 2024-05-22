using System;

namespace ET
{

    public static class B2CollisionHandlerDispatcherComponentSystem
    {
        public class B2CollisionHandlerDispatcherComponentAwakeSystem : AwakeSystem<B2CollisionHandlerDispatcherComponent>
        {
            protected override void Awake(B2CollisionHandlerDispatcherComponent self)
            {
                B2CollisionHandlerDispatcherComponent.Instance = self;
                self.Handlers = new();
                var types = EventSystem.Instance.GetTypes(typeof(B2CollisionHandlerAttribute));
                foreach (Type type in types)
                {
                    AB2CollisionHandler handler = Activator.CreateInstance(type) as AB2CollisionHandler;
                    if (handler == null)
                    {
                        Log.Error($"robot ai is not AB2CollisionHandler: {type.Name}");
                        continue;
                    }
                    
                    Log.Info(type.Name);
                    self.Handlers.Add(type.Name, handler);
                }
            }
        }
    }
}