namespace ET
{
    [BsonDeserializerRegister]
    public class RootNodeData : DecoratorNodeData
    {
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddChild<RootNode>();
        }
    }
}