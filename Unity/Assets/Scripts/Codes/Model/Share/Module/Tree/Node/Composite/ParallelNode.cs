using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class ParallelNode : Entity, INode, IAwake, IDestroy
    {
        [BsonIgnore]
        public new Entity[] Children
        {
            get
            {
                return base.Children.Values.ToArray();
            }
        }
    }
}