using GraphProcessor;
using NPBehave;

namespace ET
{
    [NodeMenuItem("Tree/Composite/Sequence", typeof(TreeGraph))]
    public class SequenceEditorNode: CompositeEditorNode
    {
        public override object Init(object[] nodes)
        {
            this.NodeData = NodeHelper.CreatNodeData("SequenceNodeData");
            return this.NodeData;
        }
    }
}