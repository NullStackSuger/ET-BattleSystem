using GraphProcessor;
using NPBehave;

namespace ET
{
    [NodeMenuItem("Tree/Task/CTest", typeof(TreeGraph))]
    public class CTestEditorNode : TaskEditorNode
    {
        public override object Init(Blackboard blackboard)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET..Client.CTestNodeData");
            return this.NodeData;
        }
    }
}