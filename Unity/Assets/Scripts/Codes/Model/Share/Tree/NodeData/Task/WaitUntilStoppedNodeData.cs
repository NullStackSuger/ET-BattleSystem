using ET.Node;
using NPBehave;

namespace ET
{
    public class WaitUntilStoppedNodeData: TaskNodeData
    {
        public override Task Init(Unit unit, Blackboard blackboard)
        {
            WaitUntilStopped waitUntilStopped = new WaitUntilStopped();
            this.NP_Node = waitUntilStopped;
            return waitUntilStopped;
        }
    }
}