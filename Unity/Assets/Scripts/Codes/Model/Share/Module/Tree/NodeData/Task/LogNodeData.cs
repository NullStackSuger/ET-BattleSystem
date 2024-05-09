namespace ET
{
    [BsonDeserializerRegister]
    public class LogNodeData : TaskNodeData
    {
        public string Message;
        
        public override void AddNode(Entity parent, TreeComponent tree)
        {
            //parent.AddComponent<LogNode, string>(this.Message);
        }
    }
}