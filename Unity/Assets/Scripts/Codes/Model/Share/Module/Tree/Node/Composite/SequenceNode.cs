using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class SequenceNode : Entity, INode, IAwake, IDestroy
    {
        [BsonIgnore]
        public Entity[] Children
        {
            get
            {
                return base.Children.Values.ToArray();
            }
        }
    }
}