using System;

namespace ET
{
    [BsonDeserializerRegister]
    public class LogNodeData : TaskNodeData
    {
        public string Message;
        
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddComponent<LogNode, string>(this.Message);
        }
    }
}