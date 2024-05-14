namespace ET.Client
{

    [NodeHandler(typeof(CreatCastNode))]
    [FriendOf(typeof(CreatCastNode))]
    public class CreatCastNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as CreatCastNode;

            Log.Info($"C_CreatCastNode {node.CastConfigId}");
            tree.Owner.GetComponent<CastComponent>().Creat(node.CastConfigId);

            await ETTask.CompletedTask;
            return true;
        }
    }

    public class CreatCastNodeAwakeSystem: AwakeSystem<CreatCastNode, int>
    {
        protected override void Awake(CreatCastNode self, int castConfigId)
        {
            self.CastConfigId = castConfigId;
        }
    }
    
    public class CreatCastNodeDestroySystem : DestroySystem<CreatCastNode>
    {
        protected override void Destroy(CreatCastNode self)
        {
            self.CastConfigId = -1;
        }
    }
}