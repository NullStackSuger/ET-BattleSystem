namespace ET
{
    [BsonDeserializerRegister]
    public class WaitNodeData : TaskNodeData
    {
        public long Seconds;

        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddComponent<WaitNode, long>(this.Seconds);
        }
    }
}