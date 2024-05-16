namespace ET
{
    [NodeHandler(typeof(WaitUntilStopNode))]
    [FriendOf(typeof(WaitUntilStopNode))]
    public class WaitUntilStopNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as WaitUntilStopNode;

            await ETTask.CompletedTask;
            return true;
        }
    }
}