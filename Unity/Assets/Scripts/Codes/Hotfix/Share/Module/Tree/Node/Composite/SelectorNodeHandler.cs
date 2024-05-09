namespace ET
{
    [NodeHandler(typeof(SelectorNode))]
    [FriendOf(typeof(SelectorNode))]
    public class SelectorNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as SelectorNode;
            
            foreach (Entity child in node.Children)
            {
                bool result = await NodeDispatcherComponent.Instance.Get(child.GetType()).Run(child, tree, cancellationToken);
                if (result == true) return true;
            }
            return false;
        }
    }
}