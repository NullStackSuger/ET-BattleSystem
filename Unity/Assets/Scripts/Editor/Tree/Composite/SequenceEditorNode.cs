using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Tree/Composite/Sequence", typeof(TreeGraph))]
    public class SequenceEditorNode: CompositeEditorNode
    {
        public override object Init(object[] nodes)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.SequenceNodeData");
            return this.NodeData;
        }
    }
}