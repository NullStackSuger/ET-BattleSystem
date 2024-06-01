using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/SubTree", typeof(ClientTreeGraph))]
    [NodeMenuItem("Task/SubTree", typeof(ServerTreeGraph))]
    public class SubTreeEditorNode : TaskEditorNode
    {
        public string Name;
        
        public override object Init()
        {
            this.NodeData = ReflectHelper.CreatNodeData("ET.SubTreeNodeData");
            ReflectHelper.SetField(this.NodeData,  ("Name", this.Name));
            return this.NodeData;
        }
    }
}