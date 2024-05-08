using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class RepeaterComponent : Entity, INodeRun, IAwake, IDestroy
    {
        [BsonIgnore]
        public Entity Child
        {
            get
            {
                return this.GetComponent<Entity>();
            }
        }

        public int LoopCount = -1; // while(true)
    }
}