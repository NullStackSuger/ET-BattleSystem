using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Composite/Sequence", typeof(ClientTreeGraph))]
    [NodeMenuItem("Composite/Sequence", typeof(ServerTreeGraph))]
    public class SequenceEditorNode: CompositeEditorNode
    {
        public override object Init(object[] nodes)
        {
            this.NodeData = ReflectHelper.CreatNodeData("ET.SequenceNodeData");
            ReflectHelper.SetField(this.NodeData, ("Children", nodes));
            return this.NodeData;
        }
    }
}