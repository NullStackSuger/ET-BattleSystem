using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Tree/Decorator/Root", typeof(TreeGraph))]
    public class RootEditorNode : DecoratorEditorNode
    {
        public override object Init(Dictionary<string, object> blackboard, object node)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.RootNodeData");
            NodeHelper.SetField(this.NodeData, "Blackboard", blackboard);
            return this.NodeData;
        }
    }
}