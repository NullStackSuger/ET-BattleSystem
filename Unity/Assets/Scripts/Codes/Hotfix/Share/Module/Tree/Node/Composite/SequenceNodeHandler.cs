namespace ET
{
    [NodeHandler(typeof(SequenceNode))]
    [FriendOf(typeof(SequenceNode))]
    [FriendOf(typeof(NodeDispatcherComponent))]
    public class SequenceNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as SelectorNode;

            foreach (Entity child in node.Children)
            {
                bool result = await NodeDispatcherComponent.Instance.NodeHandlers[child.GetType()].Run(child, tree, cancellationToken);
                if (result == false) return false;
            }
            return true;
        }
    }
}