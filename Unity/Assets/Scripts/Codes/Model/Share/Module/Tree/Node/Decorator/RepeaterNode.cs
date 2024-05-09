using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class RepeaterNode : Entity, INode, IAwake, IDestroy
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