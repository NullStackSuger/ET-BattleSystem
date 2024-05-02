using NPBehave;

namespace ET.Server
{
    public class STestNode : Task
    {
        public STestNode() : base("Test")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            Log.Info("TestNode DoStart");
        }
    }
}