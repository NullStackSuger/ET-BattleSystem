using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Tree/Task/Log", typeof(TreeGraph))]
    public class LogEditorNode : TaskEditorNode
    {
        public string Message;
        
        public override object Init(Dictionary<string, object> blackboard)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.LogNodeData");
            NodeHelper.SetField(this.NodeData,  ("Message", this.Message));
            return this.NodeData;
        }
    }
}