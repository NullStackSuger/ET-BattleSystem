using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace ET
{
    [NodeMenuItem("Decorator/Root", typeof(TreeGraph))]
    public class RootEditorNode : DecoratorEditorNode
    {
        public override object Init(Dictionary<string, object> blackboard, object node)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.RootNodeData");
            NodeHelper.SetField(this.NodeData, ("Child", node));
            return this.NodeData;
        }
    }
}