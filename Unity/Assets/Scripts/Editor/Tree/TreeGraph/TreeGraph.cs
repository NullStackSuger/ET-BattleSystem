using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using GraphProcessor;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace ET
{
    //[CreateAssetMenu(fileName = "Tree Graph", menuName = "Tree Graph")]
    public class TreeGraph : BaseGraph
    {
        [LabelText("保存路径"), GUIColor(0.1f, 0.7f, 1)] [FolderPath]
        public string SavePath = "Assets/Scripts/Editor/Tree/Save";

        [BoxGroup("行为树")] 
        public object Tree;
        
        [Button("ToNodeData", 25), GUIColor(0.4f, 0.8f, 1)]
        public void ToNodeData()
        {
            // ------------------------------------------------------------------------
            Debug.Log("----------------开始构建NodeData----------------");
            RootEditorNode rootEditorNode = FindNode<RootEditorNode>(this.nodes);
            Debug.Log("找到Root");
            List<BaseNode> nodes = new();
            Sort(ref nodes, rootEditorNode);
            Debug.Log("对Nodes排序完成");
            
            Init(nodes);
            Debug.Log("创建NodeData对象完成");
            SetData(nodes);
            Debug.Log("设置信息完成");
            
            Tree = rootEditorNode.NodeData;
            Debug.Log("构建NodeData完成");

            // ------------------------------------------------------------------------
            
            // 找到RootEditorNode
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
            
            // 后序遍历排序
            void Sort(ref List<BaseNode> nodes, BaseNode node)
            {
                foreach (BaseNode baseNode in node.GetOutputNodes())
                {
                    Sort(ref nodes, baseNode);
                }
                nodes.Add(node);
            }
            
            // 调用Init
            void Init(List<BaseNode> nodes)
            {
                foreach (BaseNode baseNode in nodes)
                {
                    if (baseNode is not EditorNodeBase node) continue;
                    
                    switch (node)
                    {
                        case TaskEditorNode task:
                            task.Init();
                            break;
                        case DecoratorEditorNode decorator:
                            foreach (BaseNode child in baseNode.GetOutputNodes())
                            {
                                decorator.Init((child as EditorNodeBase).NodeData);
                                break;
                            }
                            break;
                        case CompositeEditorNode composite:
                            List<object> temp = new();
                            foreach (BaseNode child in baseNode.GetOutputNodes())
                            {
                                temp.Add((child as EditorNodeBase).NodeData);
                            }
                            composite.Init(temp.ToArray());
                            break;
                    }
                }
            }
            
            // 设置信息(如 Child)
            void SetData(List<BaseNode> nodes)
            {
                foreach (BaseNode baseNode in nodes)
                {
                    if (baseNode is not EditorNodeBase node) continue;
                    
                    switch (node)
                    {
                        case TaskEditorNode task:
                            break;
                        case DecoratorEditorNode decorator:
                            foreach (BaseNode child in baseNode.GetOutputNodes())
                            {
                                NodeHelper.SetField(node.NodeData,"Child", (child as EditorNodeBase).NodeData);
                                break;
                            }
                            break;
                        case CompositeEditorNode composite:
                            List<object> temp = new();
                            foreach (BaseNode child in baseNode.GetOutputNodes())
                            {
                                temp.Add((child as EditorNodeBase).NodeData);
                            }
                            NodeHelper.SetField(node.NodeData,"Children", temp.ToArray());
                            break;
                    }
                }
            }
            
            // 返回RootNodeData
        }
        
        [Button("ToBson", 25), GUIColor(0.4f, 0.8f, 1)]
        public void ToBson()
        {
            if (string.IsNullOrEmpty(SavePath)) SavePath = "Assets/Scripts/Editor/Tree/Save";
            
            using (FileStream file = File.Create($"{SavePath}/{name}.bytes"))
            {
                BsonSerializer.Serialize(new BsonBinaryWriter(file), Tree);
            }
            
            Debug.Log(this.Tree.ToJson());
            Debug.Log($"保存 {SavePath}/{name}.bytes 成功");
            
            AssetDatabase.Refresh();
        }
    }
}