namespace ET
{
    [NodeHandler(typeof(LogNode))]
    [FriendOf(typeof(LogNode))]
    public class LogNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as LogNode;

            Log.Warning(node.Message);

            await ETTask.CompletedTask;
            return true;
        }
    }
}