using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/CreatCast", typeof(TreeGraph))]
    public class CreatCastEditorNode : TaskEditorNode
    {
        public int CastConfigId;
        
        public override object Init(Dictionary<string, object> blackboard)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.Server.CreatCastNodeData");
            NodeHelper.SetField(this.NodeData,  ("CastConfigId", this.CastConfigId));
            return this.NodeData;
        }
    }
}