using ET.Node;
using NPBehave;

namespace ET.Server
{
    public class STestNodeData : TaskNodeData
    {
        public override Task Init(Unit unit, Blackboard blackboard)
        {
            STestNode sTestNode = new STestNode();
            this.NP_Node = sTestNode;
            return sTestNode;
        }
    }
}