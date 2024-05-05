namespace ET.Node
{
    [BsonDeserializerRegister]
    public class STestNode : Task
    {
        //public int castConfigId = 0001; 
        public STestNode() : base("STestNode")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            Log.Info("STestNode DoStart");
            //0001
            Log.Info(this.RootNode == null);
            Log.Info(this.RootNode.Unit == null);
            Log.Info((this.RootNode.Unit as Unit).GetComponent<CastComponent>() == null);
            //(this.RootNode.Unit as Unit).GetComponent<CastComponent>().Creat(0001);
        }
    }
}