using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(TreeComponent))]
    public class RootNode : Entity, INode, IAwake, IDestroy
    {
        [BsonIgnore]
        public Entity Child
        {
            get
            {
                return this.Components.Values.First();
            }
        }
    }
}