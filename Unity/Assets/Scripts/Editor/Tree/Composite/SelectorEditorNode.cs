using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Tree/Composite/Selector", typeof(TreeGraph))]
    public class SelectorEditorNode: CompositeEditorNode
    {
        public override object Init(object[] nodes)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.SelectorNodeData");
            return this.NodeData;
        }
    }
}