using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Composite/Sequence", typeof(ClientTreeGraph))]
    [NodeMenuItem("Composite/Sequence", typeof(ServerTreeGraph))]
    public class SequenceEditorNode: CompositeEditorNode
    {
        public override object Init(object[] nodes)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.SequenceNodeData");
            NodeHelper.SetField(this.NodeData, ("Children", nodes));
            return this.NodeData;
        }
    }
}