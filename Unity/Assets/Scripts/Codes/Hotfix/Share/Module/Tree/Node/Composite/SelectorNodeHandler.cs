namespace ET
{
    [NodeHandler(typeof(SelectorNode))]
    [FriendOf(typeof(SelectorNode))]
    [FriendOf(typeof(NodeDispatcherComponent))]
    public class SelectorNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as SelectorNode;

            
            Log.Warning("Selector");
            foreach (Entity child in node.Children)
            {
                bool result = await NodeDispatcherComponent.Instance.NodeHandlers[child.GetType()].Run(child, tree, cancellationToken);
                if (result == true) return true;
            }
            await ETTask.CompletedTask;
            return false;
        }
    }
}