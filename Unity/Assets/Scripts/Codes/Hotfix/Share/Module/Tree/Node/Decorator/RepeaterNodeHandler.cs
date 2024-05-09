namespace ET
{
    [NodeHandler(typeof(RepeaterNode))]
    [FriendOf(typeof(RepeaterNode))]
    public class RepeaterNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as RepeaterNode;

            if (node.LoopCount <= -1)
            {
                while (!cancellationToken.IsCancel())
                {
                    await NodeDispatcherComponent.Instance.Get(node.Child.GetType()).Run(node.Child, tree, cancellationToken);
                }
            }
            else
            {
                int count = node.LoopCount;
                while (!cancellationToken.IsCancel() && count-- <= 0)
                {
                    await NodeDispatcherComponent.Instance.Get(node.Child.GetType()).Run(node.Child, tree, cancellationToken);
                }
            }
            return true;
        }
    }
}