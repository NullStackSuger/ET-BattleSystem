using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/Wait", typeof(ClientTreeGraph))]
    [NodeMenuItem("Task/Wait", typeof(ServerTreeGraph))]
    public class WaitEditorNode : TaskEditorNode
    {
        public long Seconds;
        
        public override object Init()
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.WaitNodeData");
            NodeHelper.SetField(this.NodeData,  ("Seconds", this.Seconds));
            return this.NodeData;
        }
    }
}