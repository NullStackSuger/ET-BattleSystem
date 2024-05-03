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
            //NP_Node = new CTestNode();
            this.NP_Node = TreeGraph.Model.CreateInstance("ET.CTestNode") as Node;
            return NP_Node as Task;
        }
    }
}