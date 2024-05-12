namespace ET
{
    [BsonDeserializerRegister]
    public class ParallelNodeData : CompositeNodeData
    {
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddChild<ParallelNode>();
        }
    }
}