using System.Collections.Generic;
using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{
    [ChildOf]
    public class TreeComponent : Entity, IAwake<string>, IDestroy
    {
        [StaticField] // Key: Name, Value: RootNode
        public static Dictionary<string, RootNode> AlreadyLoadTree = new();
        [StaticField]
        public static string TreeFilePath = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Tree/Save";
        
        [BsonIgnore]
        public RootNode Root
        {
            get
            {
                return this.Components.Values.First() as RootNode;
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