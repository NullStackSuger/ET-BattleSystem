using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class SelectorComponent : Entity, INodeRun, IAwake, IDestroy
    {
        [BsonIgnore]
        public Entity[] Children
        {
            get
            {
                return this.Components.Values.ToArray();
            }
        }
    }
}