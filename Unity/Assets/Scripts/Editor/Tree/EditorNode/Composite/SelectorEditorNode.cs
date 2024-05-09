using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Composite/Selector", typeof(TreeGraph))]
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