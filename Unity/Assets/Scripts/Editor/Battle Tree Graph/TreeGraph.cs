using System.Collections.Generic;
using System.IO;
using GraphProcessor;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using NPBehave;
using Sirenix.OdinInspector;
using UnityEngine;

namespace ET
{
    [CreateAssetMenu(fileName = "Tree Graph", menuName = "Tree Graph")]
    public class TreeGraph : BaseGraph
    {
        [LabelText("保存路径"), GUIColor(0.1f, 0.7f, 1)] [FolderPath]
        public string SavePath = "Assets/Script/Editor/Battle Tree Graph/Save";

        [BoxGroup("行为树")] 
        public NPBehave.Root Tree;
        
        [BoxGroup("行为树1")] 
        public NPBehave.Root Tree1;
        
        [Button("ToNP_Tree", 25), GUIColor(0.4f, 0.8f, 1)]
        public void ToNP_Tree()
        {
            Tree = null;
            
            RootEditorNode rootEditorNode = FindNode<RootEditorNode>(this.nodes);

            List<BaseNode> nodes = new();
            Sort(ref nodes, rootEditorNode);
            InitNodes(InitBlackboard(), nodes);

            Tree = rootEditorNode.NP_Node as NPBehave.Root;
            
            Debug.Log("构建完成");

            // 倒序遍历并排序树
            void Sort(ref List<BaseNode> nodes, BaseNode root)
            {
                if (root == null) return;
                Debug.Log(root.name);
                foreach (BaseNode baseNode in root.GetOutputNodes())
                {
                    Sort(ref nodes, baseNode);
                }
                nodes.Add(root);
            }
            
            // 初始化节点
            void InitNodes(Blackboard blackboard, List<BaseNode> nodes)
            {
                foreach (BaseNode baseNode in nodes)
                {
                    if (baseNode is not EditorNodeBase) continue;
                    
                    switch (baseNode)
                    {
                        case TaskEditorNode task:
                            task.Init(null);
                            break;
                        case DecoratorEditorNode decorator:
                            foreach (BaseNode child in baseNode.GetOutputNodes())
                            {
                                if (child is EditorNodeBase childEditorNode)
                                {
                                    Debug.Log("Decorator Node : " + decorator.name + " " + "Child is: " + childEditorNode.name);
                                    decorator.Init(blackboard, childEditorNode.NP_Node);
                                    break;
                                }
                            }
                            break;
                        case CompositeEditorNode composite:
                            List<Node> temp = new();
                            foreach (BaseNode child in baseNode.GetOutputNodes())
                            {
                                if (child is EditorNodeBase childEditorNode)
                                {
                                    Debug.Log("Composite Node : " + composite.name + " " + "Child is: " + childEditorNode.name);
                                    temp.Add(childEditorNode.NP_Node);
                                }
                            }
                            composite.Init(temp.ToArray());
                            break;
                    }
                }
            }

            Blackboard InitBlackboard()
            {
                return new Blackboard(new Clock());
            }

            // 找指定节点
            List<T> FindNodes<T>(List<BaseNode> nodes) where T : BaseNode
            {
                List<T> res = new();
                foreach (BaseNode baseNode in nodes)
                {
                    if (baseNode is T node)
                        res.Add(node);
                }
                if (res.Count <= 0) Debug.LogError("未找到指定节点");
                return res;
            }
            
            // 找指定节点
            T FindNode<T>(List<BaseNode> nodes) where T : BaseNode
            {
                foreach (BaseNode baseNode in nodes)
                {
                    if (baseNode is T node)
                        return node;
                }
                Debug.LogError("未找到指定节点");
                return null;
            }
        }
        
        [Button("ToBson", 25), GUIColor(0.4f, 0.8f, 1)]
        public void ToBson()
        {
            if (string.IsNullOrEmpty(SavePath)) SavePath = "Assets/Script/Editor/Save";
            
            using (FileStream file = File.Create($"{SavePath}/{name}.bytes"))
            {
                BsonSerializer.Serialize(new BsonBinaryWriter(file), Tree);
            }
            
            Debug.Log($"保存 {SavePath}/{name}.bytes 成功");
        }
        
        [Button("DeBson", 25), GUIColor(0.4f, 0.8f, 1)]
        public void DeBson()
        {
            try
            {
                byte[] file = File.ReadAllBytes($"{SavePath}/{name}.bytes");
                if (file.Length == 0) Debug.Log("没有读取到文件");
                Tree1 = BsonSerializer.Deserialize<NPBehave.Root>(file);
                Debug.Log($"反序列化 {SavePath}/{name}.bytes 成功");
            }
            catch (Exception e)
            {
                Debug.Log(e.ToString());
                throw;
            }
        }
    }
}