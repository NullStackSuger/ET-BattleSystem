

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

    public class LogNodeAwakeSystem: AwakeSystem<LogNode, string>
    {
        protected override void Awake(LogNode self, string message)
        {
            self.Message = message;
        }
    }
    
    public class LogNodeDestroyystem : DestroySystem<LogNode>
    {
        protected override void Destroy(LogNode self)
        {
            self.Message = "";
        }
    }
}