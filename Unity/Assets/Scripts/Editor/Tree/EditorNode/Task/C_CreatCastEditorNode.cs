using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/CreatCast", typeof(ClientTreeGraph))]
    public class C_CreatCastEditorNode : TaskEditorNode
    {
        public int CastConfigId;
        
        public override object Init()
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.Client.C_CreatCastNodeData");
            NodeHelper.SetField(this.NodeData,  ("CastConfigId", this.CastConfigId));
            return this.NodeData;
        }
    }
}