namespace ET
{
    [BsonDeserializerRegister]
    public abstract class DecoratorNodeData : BaseNodeData
    {
        public BaseNodeData Child;
    }
}