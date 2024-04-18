using NPBehave;

namespace ET.Client
{
    public class TestNode : Task
    {
        public TestNode() : base("Test")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            Log.Info("TestNode DoStart");
        }
    }
}