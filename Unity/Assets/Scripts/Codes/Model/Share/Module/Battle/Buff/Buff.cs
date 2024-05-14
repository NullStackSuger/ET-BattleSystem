using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ChildOf(typeof (BuffComponent))]
    public class Buff: Entity, IAwake<int>, IDestroy
    {
        public int ConfigId;
        
        [BsonIgnore]
        public Unit Owner;
        [BsonIgnore]
        public List<long> Targets;
    }
}