namespace ET
{
    [BsonDeserializerRegister]
    public class SelectorNodeData : CompositeNodeData
    {
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddComponent<SelectorNode>();
        }
    }
}