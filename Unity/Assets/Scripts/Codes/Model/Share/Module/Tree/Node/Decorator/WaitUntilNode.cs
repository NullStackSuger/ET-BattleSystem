using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class WaitUntilNode : Entity, INode, IAwake, IDestroy
    {
        [BsonIgnore]
        public Entity Child
        {
            get
            {
                return this.Children.Values.First();
            }
        }
    }
}