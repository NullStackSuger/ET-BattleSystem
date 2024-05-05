using NPBehave;
using Task = NPBehave.Task;

namespace ET.Client
{
    public class CTestNodeData : TaskNodeData
    {
        public override Task Init(Unit unit, Blackboard blackboard)
        {
            CTestNode cTestNode = new CTestNode();
            this.NP_Node = cTestNode;
            return cTestNode;
        }
    }
}