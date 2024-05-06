namespace ET.Node
{
    public class STestNode : Task
    {
        //public int castConfigId = 0001; 
        public STestNode() : base("STestNode")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            //0001
            //this.RootNode.Unit.GetComponent<CastComponent>().Creat(0001);
        }
    }
}