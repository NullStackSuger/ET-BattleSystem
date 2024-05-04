using GraphProcessor;
using NPBehave;
using Task = NPBehave.Task;

namespace ET
{
    [NodeMenuItem("Tree/Task/CTest", typeof(TreeGraph))]
    public class CTestEditorNode : TaskEditorNode
    {
        public override Task Init(Blackboard blackboard)
        {
            this.NP_Node = TreeGraph.Hotfix.CreateInstance("ET.Client.CTestNode") as Node;
            return NP_Node as Task;
        }
    }
}