using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace ET
{
    [NodeMenuItem("Decorator/Root", typeof(ClientTreeGraph))]
    [NodeMenuItem("Decorator/Root", typeof(ServerTreeGraph))]
    public class RootEditorNode : DecoratorEditorNode
    {
        public override object Init(object node)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.RootNodeData");
            NodeHelper.SetField(this.NodeData, ("Child", node));
            return this.NodeData;
        }
    }
}