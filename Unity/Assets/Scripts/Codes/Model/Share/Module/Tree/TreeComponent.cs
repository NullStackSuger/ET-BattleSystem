using System.Collections.Generic;

namespace ET
{

    public class TreeComponent : Entity, IAwake<string>, IDestroy
    {
        [StaticField] // Key: Name, Value: fileBytes
        public static Dictionary<string, RootNode> AlreadyLoadTree = new();
        [StaticField]
        public static string TreeFilePath = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Tree/Save";
        
        public ETCancellationToken CancellationToken;

        public RootNode Root;
    }
}