namespace ET
{
    [BsonDeserializerRegister]
    public abstract class CompositeNodeData : BaseNodeData
    {
        public object[] Children;
    }
}