using System.Collections.Generic;

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

                // Patch
            }
        }

        public class NodeDispatcherComponentDestroySystem : DestroySystem<NodeDispatcherComponent>
        {
            protected override void Destroy(NodeDispatcherComponent self)
            {
                NodeDispatcherComponent.Instance = null;
                self.Nodes.Clear();
            }
        }

        public static (Entity, NodeRun) Get(this NodeDispatcherComponent self, NodeType type)
        {
            return self.Nodes.GetValueOrDefault(type);
        }
    }
}