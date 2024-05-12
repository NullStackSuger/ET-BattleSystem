namespace ET
{
    [BsonDeserializerRegister]
    public class RepeaterNodeData : DecoratorNodeData
    {
        public int LoopCount = -1;

        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddChild<RepeaterNode, int>(this.LoopCount);
        }
    }
}