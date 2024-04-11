using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;
using Unity.Mathematics;

namespace ET.Server
{
    /// <summary>
    /// CastComponent -> Unit -> Cast
    /// </summary>
    [ComponentOf(typeof(Unit))]
    public class Cast: Entity, IAwake<int>, IDestroy, IUpdate
    {
        public int ConfigId;

        [BsonIgnore]
        public Unit Owner;
        [BsonIgnore]
        public List<long> Targets;
    }
}