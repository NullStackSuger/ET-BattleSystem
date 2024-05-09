using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class RootNode : Entity, INode, IAwake, IDestroy
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