using GraphProcessor;
using NPBehave;
using Task = NPBehave.Task;

namespace ET
{
    [NodeMenuItem("Tree/Task/STest", typeof(TreeGraph))]
    public class STestEditorNode : TaskEditorNode
    {
        public override Task Init(Blackboard blackboard)
        {
            //NP_Node = new CTestNode();
            this.NP_Node = TreeGraph.Model.CreateInstance("ET.Server.STestNode") as Node;
            return NP_Node as Task;
        }
    }
}