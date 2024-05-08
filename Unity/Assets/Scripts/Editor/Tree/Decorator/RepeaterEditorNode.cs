using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Tree/Decorator/Repeater", typeof(TreeGraph))]
    public class RepeaterEditorNode : DecoratorEditorNode
    {
        public int LoopCount = -1;
        
        public override object Init(Dictionary<string, object> blackboard, object node)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.RepeaterNodeData");
            NodeHelper.SetField(this.NodeData, ("LoopCount", this.LoopCount));
            return this.NodeData;
        }
    }
}