using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Decorator/Repeater", typeof(ClientTreeGraph))]
    [NodeMenuItem("Decorator/Repeater", typeof(ServerTreeGraph))]
    public class RepeaterEditorNode : DecoratorEditorNode
    {
        public int LoopCount = -1;
        
        public override object Init(object node)
        {
            this.NodeData = ReflectHelper.CreatNodeData("ET.RepeaterNodeData");
            ReflectHelper.SetField(this.NodeData, ("LoopCount", this.LoopCount));
            ReflectHelper.SetField(this.NodeData, ("Child", node));
            return this.NodeData;
        }
    }
}