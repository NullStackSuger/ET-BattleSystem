namespace ET
{
    [BsonDeserializerRegister]
    public class WaitUntilNodeData : DecoratorNodeData
    {
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddChild<WaitUntilNode>();
        }
    }
}