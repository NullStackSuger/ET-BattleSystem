using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;

namespace ET
{
    public class CreatNodeEditor: OdinEditorWindow
    {
        [MenuItem("ET/Creat Node Editor")]
        private static void OpenWindow()
        {
            GetWindow<CreatNodeEditor>().Show();
        }
        
        public enum NodeType
        {
            Task, Decorator, Composite
        }
        
        public class NodeField<T>
        {
            public Type Type = typeof (T);
            public T Value;
        }

        [BoxGroup("全局设置")] 
        [LabelText("EditorNode路径")][FolderPath]
        public string EditorNodePath;
        [BoxGroup("全局设置")] 
        [LabelText("NodeData路径")][FolderPath]
        public string NodeDataPath;
        [BoxGroup("全局设置")] 
        [LabelText("Node路径")][FolderPath]
        public string NodePath;
        [BoxGroup("全局设置")] 
        [LabelText("NodeHandler路径")][FolderPath]
        public string NodeHandlerPath;
        
        [BoxGroup("创建节点")]
        public string Name;
        [BoxGroup("创建节点")]
        public NodeType Type;
        [BoxGroup("创建节点")]
        public List<NodeField<object>> Fields = new();
    }
}