namespace ET
{
    [BsonDeserializerRegister]
    public class RootNodeData : DecoratorNodeData
    {
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            // 这里parent是TreeComponent
            return parent.AddComponent<RootNode>();
        }
    }
}