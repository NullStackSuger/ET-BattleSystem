namespace ET
{
    [BsonDeserializerRegister]
    public class RootNodeData : DecoratorNodeData
    {
        public int test;
        
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddComponent<RootNode>();
        }
    }
}