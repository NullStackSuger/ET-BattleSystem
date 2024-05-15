using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{

    public class BlackboardConditionNode : Entity, INode, IAwake<Operator, string, object>, IDestroy
    {
        [BsonIgnore]
        public Entity Child
        {
            get
            {
                return this.Children.Values.First();
            }
        }

        public Operator Op;
        public string Key;
        public object Value;
    }
}