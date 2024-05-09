using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/WaitUntilStop", typeof(TreeGraph))]
    public class WaitUntilStopEditorNode: TaskEditorNode
    {
        public override object Init(Dictionary<string, object> blackboard)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.WaitUntilStopNodeData");
            return this.NodeData;
        }
    }
}