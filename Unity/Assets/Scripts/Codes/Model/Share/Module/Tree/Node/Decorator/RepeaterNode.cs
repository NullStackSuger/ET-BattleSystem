using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    public class RepeaterNode : Entity, INode, IAwake<int>, IDestroy
    {
        [BsonIgnore]
        public Entity Child
        {
            get
            {
                return this.Children.Values.First();
            }
        }

        public int LoopCount = -1; // while(true)
    }
}