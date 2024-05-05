using GraphProcessor;
using NPBehave;
using UnityEngine;

namespace ET
{
    [NodeMenuItem("Tree/Decorator/Root", typeof(TreeGraph))]
    public class RootEditorNode : DecoratorEditorNode
    {
        public override object Init(Blackboard blackboard, object node)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.RootNodeData");
            NodeHelper.SetField(this.NodeData, "Blackboard", blackboard);
            return this.NodeData;
        }
    }
}