using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Tree/Composite/Parallel", typeof(TreeGraph))]
    public class ParallelEditorNode: CompositeEditorNode
    { 
        public override object Init(object[] nodes)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.ParallelNodeData");
            return this.NodeData;
        }
    }
}