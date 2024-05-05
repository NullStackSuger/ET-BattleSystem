using GraphProcessor;
using NPBehave;

namespace ET
{
    [NodeMenuItem("Tree/Task/STest", typeof(TreeGraph))]
    public class STestEditorNode : TaskEditorNode
    {
        public override object Init(Blackboard blackboard)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.STestNodeData");
            return this.NodeData;
        }
    }
}