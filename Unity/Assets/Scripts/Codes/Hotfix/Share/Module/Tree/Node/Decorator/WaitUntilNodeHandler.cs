namespace ET
{
    [NodeHandler(typeof(WaitUntilNode))]
    [FriendOf(typeof(WaitUntilNode))]
    [FriendOf(typeof(NodeDispatcherComponent))]
    public class WaitUntilNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as WaitUntilNode;

            while (!cancellationToken.IsCancel())
            {
                bool res = await NodeDispatcherComponent.Instance.NodeHandlers[node.Child.GetType()].Run(node.Child, tree, cancellationToken);
                if (res) return true;
            }

            return false;
        }
    }
}