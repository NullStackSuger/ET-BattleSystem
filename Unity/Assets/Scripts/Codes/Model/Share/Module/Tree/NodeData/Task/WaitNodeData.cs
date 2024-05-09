namespace ET
{
    [BsonDeserializerRegister]
    public class WaitNodeData : TaskNodeData
    {
        public float Seconds;

        public override void AddNode(Entity parent, TreeComponent tree)
        {
            
        }
    }
}