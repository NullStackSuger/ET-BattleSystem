namespace ET
{
    [BsonDeserializerRegister]
    public class WaitUntilStopNodeData : TaskNodeData
    {
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddComponent<WaitUntilStopNode>();
        }
    }
}