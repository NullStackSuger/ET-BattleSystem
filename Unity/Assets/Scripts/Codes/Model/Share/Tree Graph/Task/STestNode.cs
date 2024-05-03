using NPBehave;

namespace ET
{
    [BsonDeserializerRegister]
    public class STestNode : Task
    {
        public STestNode() : base("STestNode")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            Log.Info("STestNode DoStart");
        }
    }
}