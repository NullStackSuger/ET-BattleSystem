namespace ET
{
    [NodeHandler(typeof(ParallelNode))]
    [FriendOf(typeof(ParallelNode))]
    [FriendOf(typeof(NodeDispatcherComponent))]
    public class ParallelNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as ParallelNode;

            foreach (Entity child in node.Children)
            {
                NodeDispatcherComponent.Instance.NodeHandlers[child.GetType()].Run(child, tree, cancellationToken).Coroutine();
            }

            await ETTask.CompletedTask;
            return true;
        }
    }
}