using ET.Node;
using NPBehave;

namespace ET
{
    [BsonDeserializerRegister]
    public class WaitUntilStoppedNodeData: TaskNodeData
    {
        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            WaitUntilStopped waitUntilStopped = new WaitUntilStopped();
            this.NP_Node = waitUntilStopped;
            return waitUntilStopped;
        }
    }
}