namespace ET
{
    [BsonDeserializerRegister]
    public class BlackboardConditionNodeData : DecoratorNodeData
    {
        public int Op;
        public string Key;
        public object Value;
        
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddChild<BlackboardConditionNode, Operator, string, object>((Operator)Op, this.Key, this.Value);
        }
    }
}