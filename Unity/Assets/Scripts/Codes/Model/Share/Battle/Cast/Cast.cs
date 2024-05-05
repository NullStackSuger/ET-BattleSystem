using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    /// <summary>
    /// CastComponent -> Unit -> Cast
    /// </summary>
    [ComponentOf(typeof(Unit))]
    public class Cast: Entity, IAwake<int>, IDestroy
    {
        public int ConfigId;

        [BsonIgnore]
        public Unit Owner;
        [BsonIgnore]
        public List<long> Targets;
    }
}