namespace ET.Client
{
    [BsonDeserializerRegister]
    public class C_CreatCastNodeData : TaskNodeData
    {
        public int CastConfigId;
        
        [EnableAccessEntiyChild]
        public override Entity AddNode(Entity parent, TreeComponent tree)
        {
            return parent.AddChild<CreatCastNode, int>(this.CastConfigId);
        }
    }
}