using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Composite/Parallel", typeof(ClientTreeGraph))]
    [NodeMenuItem("Composite/Parallel", typeof(ServerTreeGraph))]
    public class ParallelEditorNode: CompositeEditorNode
    { 
        public override object Init(object[] nodes)
        {
            this.NodeData = ReflectHelper.CreatNodeData("ET.ParallelNodeData");
            ReflectHelper.SetField(this.NodeData, ("Children", nodes));
            return this.NodeData;
        }
    }
}