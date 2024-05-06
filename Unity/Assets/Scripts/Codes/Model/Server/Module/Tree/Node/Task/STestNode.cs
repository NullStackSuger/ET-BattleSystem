namespace ET.Node
{
    public class STestNode : Task
    {
        public int CastConfigId = 0001;
        
        public STestNode() : base("STestNode")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            
            // 问题: 想在DoStart调用hotfix代码
            // push to github
            // Try move Battle to Core
            // then can use CastCompoent in Model
            //0001
            //this.RootNode.Unit.GetComponent<CastComponent>().Creat(0001);
        }
    }
}