namespace ET
{
    [BsonDeserializerRegister]
    public class SequenceNodeData : CompositeNodeData
    {
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddChild<SequenceNode>();
        }
    }
}