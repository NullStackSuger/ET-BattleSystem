namespace ET
{
    [BsonDeserializerRegister]
    public class RepeaterNodeData : DecoratorNodeData
    {
        public int LoopCount = -1;

        public override void AddNode(Entity parent, TreeComponent tree)
        {
            
        }
    }
}