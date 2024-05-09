namespace ET
{
    [NodeHandler(typeof(WaitNode))]
    [FriendOf(typeof(WaitNode))]
    public class WaitNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as WaitNode;

            // 参考AI_Attack写的 但是是Client 这是Server 不确定对不对
            await TimerComponent.Instance.WaitAsync(node.Seconds, cancellationToken);
            
            return true;
        }
    }
    
    public class WaitNodeAwakeSystem: AwakeSystem<WaitNode, long>
    {
        protected override void Awake(WaitNode self, long seconds)
        {
            self.Seconds = seconds;
        }
    }
    
    public class WaitNodeDestroySystem : DestroySystem<WaitNode>
    {
        protected override void Destroy(WaitNode self)
        {
            self.Seconds = 0;
        }
    }
}