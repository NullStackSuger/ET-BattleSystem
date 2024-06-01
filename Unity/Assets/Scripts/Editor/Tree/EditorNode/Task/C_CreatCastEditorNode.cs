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
            this.NodeData = ReflectHelper.CreatNodeData("ET.Client.C_CreatCastNodeData");
            ReflectHelper.SetField(this.NodeData,  ("CastConfigId", this.CastConfigId));
            return this.NodeData;
        }
    }
}