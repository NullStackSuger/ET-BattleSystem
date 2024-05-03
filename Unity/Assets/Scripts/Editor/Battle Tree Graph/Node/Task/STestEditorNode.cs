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
            this.NP_Node = TreeGraph.Model.CreateInstance("ET.STestNode") as Node;
            return NP_Node as Task;
        }
    }
}