using System.Collections.Generic;
using System.IO;
using NPBehave;
using Exception = NPBehave.Exception;

namespace ET
{

    public static class TreeFactory
    {
        private const string ClientPath = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Tree/Save";
        private const string ServerPath = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Tree/Save";
        private const string ViewPath   = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Tree/Save";
        
        // TODO: 这里应该加一个字典, 每次Creat先看字典里有没有
        
        /// <summary>
        /// 构建运行时行为树
        /// </summary>
        /// <param name="treeName">之后用enum表示, 编辑器构建时加一个enum元素</param>
        /// <returns></returns>
        public static TreeComponent Creat(string name, Unit unit)
        {
            RootNodeData root;
            try
            {
                byte[] file;
                if (name.StartsWith('C'))
                    file = File.ReadAllBytes($"{ClientPath}/{name}.bytes");
                else if (name.StartsWith('S'))
                    file = File.ReadAllBytes($"{ServerPath}/{name}.bytes");
                else // name.StartsWith('V')
                    file = File.ReadAllBytes($"{ViewPath}/{name}.bytes");
                
                if (file.Length == 0) Log.Info("没有读取到文件");
                
                // ----------------------------------------------------------------
                
                root = MongoHelper.Deserialize<RootNodeData>(file);
                
                Log.Info($"反序列化{name}.bytes 成功");
            }
            catch (Exception e)
            {
                Log.Info(e.ToString());
                throw;
            }
            
            // ----------------------------------------------------------------

            TreeComponent result = unit.AddComponent<TreeComponent>();
            Blackboard blackboard = InitBlackboard();
            List<NodeData> nodeDatas = new();
            Sort(ref nodeDatas, root);
            Init(nodeDatas, blackboard);

            Log.Info("构建NP_Tree成功");
            return result;
            
            // 添加TreeComponent组件
            
            // 生成黑板
            Blackboard InitBlackboard()
            {
                Blackboard blackboard = new(new Clock());
                
                // 给黑板赋值

                return blackboard;
            }
            // 找到RootNodeData
            
            // 倒序排序
            void Sort(ref List<NodeData> nodeDatas, NodeData nodeData)
            {
                switch (nodeData)
                {
                    case TaskNodeData:
                        break;
                    case DecoratorNodeData decorator:
                        Sort(ref nodeDatas, decorator.Child);
                        break;
                    case CompositeNodeData composite:
                        foreach (NodeData child in composite.Children)
                        {
                            Sort(ref nodeDatas, child);
                        }
                        break;
                }
                nodeDatas.Add(nodeData);
            }
            
            // 遍历Init
            void Init(List<NodeData> nodeDatas, Blackboard blackboard)
            {
                foreach (NodeData nodeData in nodeDatas)
                {
                    nodeData.Init(unit, blackboard);
                }
            }
            
            // 返回RootNodeData.NP_Node as ET.Node.Root
        }
    }
}