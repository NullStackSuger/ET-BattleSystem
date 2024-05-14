using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Composite/Selector", typeof(ClientTreeGraph))]
    [NodeMenuItem("Composite/Selector", typeof(ServerTreeGraph))]
    public class SelectorEditorNode: CompositeEditorNode
    {
        public override object Init(object[] nodes)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.SelectorNodeData");
            NodeHelper.SetField(this.NodeData, ("Children", nodes));
            return this.NodeData;
        }
    }
}