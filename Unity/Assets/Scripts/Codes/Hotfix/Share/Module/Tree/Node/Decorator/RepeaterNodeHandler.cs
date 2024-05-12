namespace ET
{
    [NodeHandler(typeof(RepeaterNode))]
    [FriendOf(typeof(RepeaterNode))]
    [FriendOf(typeof(NodeDispatcherComponent))]
    public class RepeaterNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as RepeaterNode;
            
            if (node.LoopCount <= -1)
            {
                while (!cancellationToken.IsCancel())
                {
                    await NodeDispatcherComponent.Instance.NodeHandlers[node.Child.GetType()].Run(node.Child, tree, cancellationToken);
                }
            }
            else
            {
                int count = node.LoopCount;
                while (!cancellationToken.IsCancel() && count-- > 0)
                {
                    await NodeDispatcherComponent.Instance.NodeHandlers[node.Child.GetType()].Run(node.Child, tree, cancellationToken);
                }
            }
            return true;
        }
    }

    public class RepeaterNodeAwakeSystem : AwakeSystem<RepeaterNode, int>
    {
        protected override void Awake(RepeaterNode self, int count)
        {
            self.LoopCount = count;
        }
    }
    
    public class RepeaterNodeDestroySystem : DestroySystem<RepeaterNode>
    {
        protected override void Destroy(RepeaterNode self)
        {
            self.LoopCount = 0;
        }
    }
}