namespace ET
{
    [NodeHandler(typeof(SubTreeNode))]
    [FriendOf(typeof(SubTreeNode))]
    public class SubTreeNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as SubTreeNode;
            return await node.SubTree.Start();
        }
    }
    
    public class SubTreeNodeAwakeSystem: AwakeSystem<SubTreeNode, string,ETCancellationToken, BlackBoard>
    {
        protected override void Awake(SubTreeNode self, string name, ETCancellationToken cancellationToken, BlackBoard blackBoard)
        {
            self.Name = name;
            // 添加TreeComponent子物体
            self.AddChild<TreeComponent, string, ETCancellationToken, BlackBoard>(name, cancellationToken, blackBoard);
        }
    }
    
    public class SubTreeNodeDestroySystem : DestroySystem<SubTreeNode>
    {
        protected override void Destroy(SubTreeNode self)
        {
            self.RemoveChild(self.SubTree.Id);
            self.Name = "";
        }
    }
}