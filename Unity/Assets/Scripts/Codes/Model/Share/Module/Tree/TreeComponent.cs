using MongoDB.Bson.Serialization.Attributes;
using NPBehave;

namespace ET
{
    /// <summary>
    /// 用于管理树
    /// </summary>
    [ComponentOf(typeof(Unit))]
    public class TreeComponent: Entity, IAwake
    {
        [BsonIgnore]
        public Unit Unit
        {
            get
            {
                return this.GetParent<Unit>();
            }
        }

        [BsonIgnore]
        public ET.Node.Root Root;

        [BsonIgnore]
        public Blackboard Blackboard;
    }
}