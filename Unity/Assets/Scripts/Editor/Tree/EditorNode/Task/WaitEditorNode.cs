using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/Wait", typeof(TreeGraph))]
    public class WaitEditorNode : TaskEditorNode
    {
        public long Seconds;
        
        public override object Init(Dictionary<string, object> blackboard)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.WaitNodeData");
            NodeHelper.SetField(this.NodeData,  ("Seconds", this.Seconds));
            return this.NodeData;
        }
    }
}