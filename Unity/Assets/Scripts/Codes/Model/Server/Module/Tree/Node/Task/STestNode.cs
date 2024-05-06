namespace ET.Node
{
    [FriendOf(typeof(Node))]
    public class STestNode : Task
    {
        public int CastConfigId = 0001;

        public STestNode() : base("STestNode")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            
            //(this.RootNode.Unit as Unit).GetComponent<CastComponent>().Creat(CastConfigId);
        }
    }
}