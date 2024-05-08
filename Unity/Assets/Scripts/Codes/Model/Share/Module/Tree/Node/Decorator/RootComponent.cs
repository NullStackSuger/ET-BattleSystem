using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class RootComponent : Entity, INodeRun, IAwake, IDestroy
    {
        [BsonIgnore]
        public Entity Child
        {
            get
            {
                return this.GetComponent<Entity>();
            }
        }
    }
}