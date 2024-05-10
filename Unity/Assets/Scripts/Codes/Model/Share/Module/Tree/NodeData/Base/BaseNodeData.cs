namespace ET
{
    [BsonDeserializerRegister]
    public abstract class BaseNodeData
    {
        // 如果要调用AddComponent, 在方法上添加[EnableAccessEntityChild]
        // 给parent添加子组件
        // returns parent
        public abstract Entity AddNode(Entity parent, TreeComponent tree); 
    }
}