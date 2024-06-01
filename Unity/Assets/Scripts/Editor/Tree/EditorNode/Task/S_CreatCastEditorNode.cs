using System.Collections.Generic;
using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Task/CreatCast", typeof(ServerTreeGraph))]
    public class S_CreatCastEditorNode : TaskEditorNode
    {
        public int CastConfigId;
        
        public override object Init()
        {
            this.NodeData = ReflectHelper.CreatNodeData("ET.Server.S_CreatCastNodeData");
            ReflectHelper.SetField(this.NodeData,  ("CastConfigId", this.CastConfigId));
            return this.NodeData;
        }
    }
}