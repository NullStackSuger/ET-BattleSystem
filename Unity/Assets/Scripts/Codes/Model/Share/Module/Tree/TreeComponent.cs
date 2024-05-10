using System.Collections.Generic;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class TreeComponent : Entity, IAwake<string>, IDestroy
    {
        [StaticField] // Key: Name, Value: fileBytes
        public static Dictionary<string, RootNode> AlreadyLoadTree = new();
        [StaticField]
        public static string TreeFilePath = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Tree/Save";
        
        [BsonIgnore]
        public RootNode Root
        {
            get
            {
                return this.GetComponent<RootNode>();
            }
        }

        [BsonIgnore]
        public Unit Owner
        {
            get
            {
                return this.GetParent<Unit>();
            }
        }
        
        public ETCancellationToken CancellationToken;
    }
}