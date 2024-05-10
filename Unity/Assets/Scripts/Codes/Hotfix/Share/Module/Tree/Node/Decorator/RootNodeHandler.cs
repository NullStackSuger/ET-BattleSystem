namespace ET
{
    [NodeHandler(typeof(RootNode))]
    [FriendOf(typeof(RootNode))]
    [FriendOf(typeof(NodeDispatcherComponent))]
    public class RootNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as RootNode;

            // Init SomeThing

            return await NodeDispatcherComponent.Instance.NodeHandlers[node.Child.GetType()].Run(node.Child, tree, cancellationToken);
        }
    }
}