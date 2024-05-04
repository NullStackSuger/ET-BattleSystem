using MongoDB.Bson.Serialization.Attributes;
using NPBehave;

namespace ET.Client
{
    [Node]
    [BsonDeserializerRegister]
    public class CTestNode : Task
    {
        public CTestNode() : base("CTestNode")
        {
        }

        protected override void DoStart()
        {
            base.DoStart();
            Log.Info("CTestNode DoStart");
        }
    }
}