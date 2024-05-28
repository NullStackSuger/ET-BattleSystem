using System;

namespace ET
{
    [FriendOf(typeof(NodeDispatcherComponent))]
    public static class NodeDispatcherComponentSystem
    {
        public class NodeDispatcherComponentAwakeSystem : AwakeSystem<NodeDispatcherComponent>
        {
            protected override void Awake(NodeDispatcherComponent self)
            {
                NodeDispatcherComponent.Instance = self;
                self.NodeHandlers ??= new();
                self.Load();
            }
        }

        public class NodeDispatcherComponentDestroySystem : DestroySystem<NodeDispatcherComponent>
        {
            protected override void Destroy(NodeDispatcherComponent self)
            {
                NodeDispatcherComponent.Instance = null;
                self.NodeHandlers.Clear();
            }
        }

        public class NodeDispatcherComponentLoadSystem : LoadSystem<NodeDispatcherComponent>
        {
            protected override void Load(NodeDispatcherComponent self)
            {
                self.Load();
            }
        }

        private static void Load(this NodeDispatcherComponent self)
        {
            self.NodeHandlers.Clear();
            var handlers = EventSystem.Instance.GetTypes(typeof (NodeHandlerAttribute));
            foreach (Type type in handlers)
            {
                ANodeHandler aNodeHandler = Activator.CreateInstance(type) as ANodeHandler;
                if (aNodeHandler == null)
                {
                    Log.Error($"robot ai is not ANodeHandler: {type.Name}");
                    continue;
                }
                
                // 获取NodeHandlerAttribute.NodeType
                NodeHandlerAttribute handler = type.GetCustomAttributes(typeof (NodeHandlerAttribute), false)[0] as NodeHandlerAttribute;
                
                self.NodeHandlers.Add(handler.NodeType,  aNodeHandler);
            }
        }
    }
}