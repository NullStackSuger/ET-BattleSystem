using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET.Server
{
    [ChildOf(typeof (BuffComponent))]
    public class Buff: Entity, IAwake<int>, IDestroy, IUpdate
    {
        public int ConfigId;
        
        [BsonIgnore]
        public Unit Owner;
        [BsonIgnore]
        public List<long> Targets;
    }
}