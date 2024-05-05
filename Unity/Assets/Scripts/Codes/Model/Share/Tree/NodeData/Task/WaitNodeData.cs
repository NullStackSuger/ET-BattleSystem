using ET.Node;
using NPBehave;

namespace ET
{
    [BsonDeserializerRegister]
    public class WaitNodeData : TaskNodeData
    {
        public float Seconds;
        
        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            Wait wait = new Wait(Seconds);
            this.NP_Node = wait;
            return wait;
        }
    }
}