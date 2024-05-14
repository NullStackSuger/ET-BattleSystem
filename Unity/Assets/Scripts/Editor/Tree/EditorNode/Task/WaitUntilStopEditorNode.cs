using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/WaitUntilStop", typeof(ClientTreeGraph))]
    [NodeMenuItem("Task/WaitUntilStop", typeof(ServerTreeGraph))]
    public class WaitUntilStopEditorNode: TaskEditorNode
    {
        public override object Init()
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.WaitUntilStopNodeData");
            return this.NodeData;
        }
    }
}