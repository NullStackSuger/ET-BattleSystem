using MongoDB.Bson.Serialization.Attributes;
using NPBehave;

namespace ET.Client
{
    public class CTestNode : Task
    {
        public CTestNode() : base("Test")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            Log.Info("TestNode DoStart");
        }
    }
}