namespace ET
{
    [BsonDeserializerRegister]
    public abstract class BaseNodeData
    {
        // 如果要调用AddChild, 在方法上添加[EnableAccessEntityChild]
        // 给parent添加子物体
        // returns parent
        public abstract Entity AddNode(Entity parent, TreeComponent tree); 
    }
}