namespace ET
{
    [BsonDeserializerRegister]
    public abstract class BaseNodeData
    {
        public abstract void AddNode(Entity parent, TreeComponent tree); // 给parent添加子组件
    }
}