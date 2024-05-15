using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/Log", typeof(ClientTreeGraph))]
    [NodeMenuItem("Task/Log", typeof(ServerTreeGraph))]
    public class LogEditorNode : TaskEditorNode
    {
        public string Message;
        
        public override object Init()
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.LogNodeData");
            NodeHelper.SetField(this.NodeData,  ("Message", this.Message));
            return this.NodeData;
        }
    }
}