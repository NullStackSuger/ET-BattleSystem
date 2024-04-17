using ET.Server;
using GraphProcessor;
using NPBehave;
using Task = NPBehave.Task;

namespace ET
{
    [NodeMenuItem("Tree/Task/Test", typeof(TreeGraph))]
    public class TestEditorNode : TaskEditorNode
    {
        public override Task Init(Unit unit, Blackboard blackboard)
        {
            NP_Node = new TestNode();
            return NP_Node as Task;
        }
    }
}