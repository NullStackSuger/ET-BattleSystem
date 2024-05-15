namespace ET
{
    [BsonDeserializerRegister]
    [FriendOf(typeof(TreeComponent))]
    public class SubTreeNodeData : TaskNodeData
    {
        public string Name;

        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddChild<SubTreeNode, string, ETCancellationToken, BlackBoard>(this.Name, tree.CancellationToken, tree.BlackBoard);
        }
    }
}