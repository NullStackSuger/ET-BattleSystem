using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Task/WaitUntilStopped", typeof(TreeGraph))]
    public class WaitUntilStoppedEditorNode: TaskEditorNode
    {
        public override object Init(Blackboard blackboard)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.WaitUntilStoppedNodeData");
            return this.NodeData;
        }
    }
}