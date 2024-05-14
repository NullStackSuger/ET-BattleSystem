using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Composite/Parallel", typeof(ClientTreeGraph))]
    [NodeMenuItem("Composite/Parallel", typeof(ServerTreeGraph))]
    public class ParallelEditorNode: CompositeEditorNode
    { 
        public override object Init(object[] nodes)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.ParallelNodeData");
            NodeHelper.SetField(this.NodeData, ("Children", nodes));
            return this.NodeData;
        }
    }
}