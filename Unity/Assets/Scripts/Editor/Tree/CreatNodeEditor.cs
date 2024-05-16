using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Serialization;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public class CreatNodeEditor: OdinMenuEditorWindow
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
        public enum BelongTo
        {
            Share, Server, Client
        }
        
        public class NodeItem
        {
            [FoldoutGroup("节点信息")]
            public string Name;
            [FoldoutGroup("节点信息")]
            public NodeType Type;
            [FoldoutGroup("节点信息")]
            public BelongTo BelongTo;
            [FoldoutGroup("节点信息")][ShowInInspector]
            public Dictionary<Type, string> Fields = new();
            
            [LabelText("EditorNode路径")][ReadOnly]
            public string EditorNodePath = "";
            [LabelText("NodeData路径")][ReadOnly]
            public string NodeDataPath = "";
            [LabelText("Node路径")][ReadOnly]
            public string NodePath = "";
            [LabelText("NodeHandler路径")][ReadOnly]
            public string NodeHandlerPath = "";
            
            private void SaveFile(string path, string name, string context)
            {
                if (File.Exists(path + "/" + name)) File.Delete($"{path}/{name}");

                using FileStream fs = File.Create($"{path}/{name}");
                fs.Write(Encoding.UTF8.GetBytes(context));
            }

            private void SetPath()
            {
                switch (this.BelongTo)
                {
                    case BelongTo.Share:
                        EditorNodePath = Setting.EditorNodeSharePath;
                        NodeDataPath = Setting.NodeDataSharePath;
                        NodePath = Setting.NodeSharePath;
                        NodeHandlerPath = Setting.NodeHandlerSharePath;
                        break;
                    case BelongTo.Client:
                        EditorNodePath = Setting.EditorNodeClientPath;
                        NodeDataPath = Setting.NodeDataClientPath;
                        NodePath = Setting.NodeClientPath;
                        NodeHandlerPath = Setting.NodeHandlerClientPath;
                        break;
                    case BelongTo.Server:
                        EditorNodePath = Setting.EditorNodeServerPath;
                        NodeDataPath = Setting.NodeDataServerPath;
                        NodePath = Setting.NodeServerPath;
                        NodeHandlerPath = Setting.NodeHandlerServerPath;
                        break;
                }
            }

            [Button("Apply", 25), GUIColor(0.4f, 0.8f, 1)]
            public void Apply()
            {
                if (this.EditorNodePath == string.Empty || this.NodeDataPath == string.Empty || this.NodePath == string.Empty || this.NodeHandlerPath == string.Empty)
                    this.SetPath();
                
                StringBuilder str = new();
                #region Build EditorNode
                str.AppendLine("using GraphProcessor;");
                str.AppendLine("namespace ET");
                str.AppendLine("{");
                str.AppendLine($"    [NodeMenuItem(\"{this.Type}/{this.Name}\", typeof(ClientTreeGraph))]");
                str.AppendLine($"    [NodeMenuItem(\"{this.Type}/{this.Name}\", typeof(ServerTreeGraph))]");
                str.AppendLine($"    public class {this.Name}EditorNode : {this.Type}EditorNode");
                str.AppendLine("    {");
                foreach (var field in this.Fields)
                {
                    str.AppendLine($"        public {field.Key} {field.Value};");
                }
                str.AppendLine("        public override object Init()");
                str.AppendLine("        {");
                str.AppendLine($"            this.NodeData = NodeHelper.CreatNodeData(\"ET.{this.Name}NodeData\");");
                foreach (var field in this.Fields)
                {
                    str.AppendLine($"            NodeHelper.SetField(this.NodeData,  (\"{field.Value}\", this.{field.Value}));");
                }
                str.AppendLine("            return this.NodeData;");
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile($"{this.EditorNodePath}/{this.Type}", $"{this.Name}EditorNode.cs", str.ToString());

                str.Clear();
                #endregion

                #region Build NodeData
                str.AppendLine("namespace ET");
                str.AppendLine("{");
                str.AppendLine("    [BsonDeserializerRegister]");
                str.AppendLine($"    public class {this.Name}NodeData : {this.Type}NodeData");
                str.AppendLine("    {");
                foreach (var field in this.Fields)
                {
                    str.AppendLine($"        public {field.Key} {field.Value};");
                }
                str.AppendLine();
                str.AppendLine("        [EnableAccessEntiyChild]");
                str.AppendLine("        public override Entity AddNode(Entity parent, TreeComponent tree)");
                str.AppendLine("        {");
                str.Append($"            return parent.AddChild<{this.Name}Node");
                foreach (Type type in this.Fields.Keys)
                {
                    str.Append($", {type}");
                }

                str.Append(">(");
                string tmp = "";
                foreach (string name in this.Fields.Values)
                {
                    tmp += $"this.{name}, ";
                }

                if (tmp.Length > 2)
                {
                    tmp = tmp.Substring(0, tmp.Length - 2);
                    str.Append(tmp);
                }

                str.Append(");");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile($"{this.NodeDataPath}/{this.Type}", $"{this.Name}NodeData.cs", str.ToString());

                str.Clear();
                #endregion

                #region Build Node
                str.AppendLine("namespace ET");
                str.AppendLine("{");
                str.Append($"    public class {this.Name}Node : Entity, INode, IAwake");
                tmp = "";
                foreach (Type type in this.Fields.Keys)
                {
                    tmp += $"{type}, ";
                }

                if (tmp.Length > 2)
                {
                    tmp = tmp.Substring(0, tmp.Length - 2);
                    str.Append("<");
                    str.Append(tmp);
                    str.Append(">");
                }

                str.Append(", IDestroy");
                str.AppendLine();
                str.AppendLine("    {");
                foreach (var field in this.Fields)
                {
                    str.AppendLine($"        public {field.Key} {field.Value};");
                }
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile($"{this.NodePath}/{this.Type}", $"{this.Name}Node.cs", str.ToString());

                str.Clear();
                #endregion

                #region Build NodeHandler
                str.AppendLine("namespace ET");
                str.AppendLine("{");
                str.AppendLine($"    [NodeHandler(typeof({this.Name}Node))]");
                str.AppendLine($"    [FriendOf(typeof({this.Name}Node))]");
                str.AppendLine($"    public class {this.Name}NodeHandler : ANodeHandler");
                str.AppendLine("    {");
                str.AppendLine("        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)");
                str.AppendLine("        {");
                str.AppendLine($"            var node = iNode as {this.Name}Node;");
                str.AppendLine();
                str.AppendLine("            Log.Warning(node.ToString());");
                str.AppendLine();
                str.AppendLine("            await ETTask.CompletedTask;");
                str.AppendLine("            return true;");
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine();
                str.Append($"    public class {this.Name}NodeAwakeSystem: AwakeSystem<{this.Name}Node");
                foreach (Type type in this.Fields.Keys)
                {
                    str.Append($", {type}");
                }
                str.Append(">");
                str.AppendLine();
                str.AppendLine("    {");
                str.Append($"        protected override void Awake({this.Name}Node self");
                foreach (var field in this.Fields)
                {
                    str.Append($", {field.Key} {char.ToLower(field.Value[0])}{field.Value.Substring(1, field.Value.Length - 1)}");
                }
                str.Append(")");
                str.AppendLine();
                str.AppendLine("        {");
                foreach (var field in this.Fields)
                {
                    str.AppendLine($"            self.{field.Value} = {char.ToLower(field.Value[0])}{field.Value.Substring(1, field.Value.Length - 1)};");
                }
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine();
                str.AppendLine($"    public class {this.Name}NodeDestroySystem : DestroySystem<{this.Name}Node>");
                str.AppendLine("    {");
                str.AppendLine($"        protected override void Destroy({this.Name}Node self)");
                str.AppendLine("        {");
                str.AppendLine("            // Do SomeThing");
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");

                SaveFile($"{this.NodeHandlerPath}/{this.Type}", $"{this.Name}NodeHandler.cs", str.ToString());
                #endregion
            }

            [Button("Delete", 25), GUIColor(0.4f, 0.8f, 1)]
            public void Delete()
            {
                if (this.EditorNodePath == "" || this.NodeDataPath == "" || this.NodePath == "" || this.NodeHandlerPath == "")
                    this.SetPath();
                
                string path = $"{this.EditorNodePath}/{this.Type}/{this.Name}EditorNode.cs";
                if (File.Exists(path)) File.Delete(path);
                path = $"{this.NodeDataPath}/{this.Type}/{this.Name}NodeData.cs";
                if (File.Exists(path)) File.Delete(path);
                path = $"{this.NodePath}/{this.Type}/{this.Name}Node.cs";
                if (File.Exists(path)) File.Delete(path);
                path = $"{this.NodeHandlerPath}/{this.Type}/{this.Name}NodeHandler.cs";
                if (File.Exists(path)) File.Delete(path);
            }

            public NodeItem(string name, NodeType type, BelongTo belongTo, params (Type, string)[] fields)
            {
                this.Name = name;
                this.Type = type;
                this.BelongTo = belongTo;
                foreach (var field in fields)
                {
                    Fields.Add(field.Item1, field.Item2);
                }
                
                this.SetPath();
            }

            /// <summary>
            /// 这个用于创建新的NodeItem
            /// </summary>
            public NodeItem()
            {
            }
        }

        public class Setting
        {
            [BoxGroup("EditorNode路径")][LabelText("Share")] [FolderPath][StaticField][ShowInInspector]
            public static string EditorNodeSharePath = "Assets/Scripts/Editor/Tree/EditorNode";
            [BoxGroup("EditorNode路径")][LabelText("Server")] [FolderPath][StaticField][ShowInInspector]
            public static string EditorNodeServerPath = "Assets/Scripts/Editor/Tree/EditorNode";
            [BoxGroup("EditorNode路径")][LabelText("Client")] [FolderPath][StaticField][ShowInInspector]
            public static string EditorNodeClientPath = "Assets/Scripts/Editor/Tree/EditorNode";

            [BoxGroup("NodeData路径")][LabelText("Share")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeDataSharePath = "Assets/Scripts/Codes/Model/Share/Module/Tree/NodeData";
            [BoxGroup("NodeData路径")][LabelText("Server")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeDataServerPath = "Assets/Scripts/Codes/Model/Server/Module/Tree/NodeData";
            [BoxGroup("NodeData路径")][LabelText("Client")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeDataClientPath = "Assets/Scripts/Codes/Model/Client/Module/Tree/NodeData";

            
            [BoxGroup("Node路径")][LabelText("Share")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeSharePath = "Assets/Scripts/Codes/Model/Share/Module/Tree/Node";
            [BoxGroup("Node路径")][LabelText("Server")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeServerPath = "Assets/Scripts/Codes/Model/Server/Module/Tree/Node";
            [BoxGroup("Node路径")][LabelText("Client")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeClientPath = "Assets/Scripts/Codes/Model/Client/Module/Tree/Node";
            
            [BoxGroup("NodeHandler路径")][LabelText("Share")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeHandlerSharePath = "Assets/Scripts/Codes/Hotfix/Share/Module/Tree/Node";
            [BoxGroup("NodeHandler路径")][LabelText("Server")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeHandlerServerPath = "Assets/Scripts/Codes/Hotfix/Server/Module/Tree/Node";
            [BoxGroup("NodeHandler路径")][LabelText("Client")] [FolderPath][StaticField][ShowInInspector]
            public static string NodeHandlerClientPath = "Assets/Scripts/Codes/Hotfix/Client/Module/Tree/Node";
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            tree.Add("Setting", new Setting());
            tree.Add("Creat New", new NodeItem());
            
            // Add Nodes
            string name = "";
            NodeType nodeType = NodeType.Task;
            BelongTo belongTo = BelongTo.Share;
            FieldInfo[] fieldInfos = null;
            List<(Type, string)> fields = new();
            foreach (Type type in NodeHelper.GetAllNodes())
            {
                // 获取Type名 : ET.XXXNodeData
                name = type.ToString().Substring(3, type.ToString().Length - 3 - 4 - 4);
                
                // 获取NodeType : 获取父类类型
                switch (type.BaseType.ToString())
                {
                    case "ET.CompositeNodeData":
                        nodeType = NodeType.Composite;
                        break;
                    case "ET.DecoratorNodeData":
                        nodeType = NodeType.Decorator;
                        break;
                    case "ET.TaskNodeData":
                        nodeType = NodeType.Task;
                        break;
                    default: continue;
                }
                
                // 获取Node作用范围
                if (name.StartsWith("Client.")) belongTo = BelongTo.Client;
                else if (name.StartsWith("Server.")) belongTo = BelongTo.Server;
                else belongTo = BelongTo.Share;
                
                // 遍历获取属性 : 反射获取属性
                fieldInfos = type.GetFields();
                foreach (FieldInfo info in fieldInfos)
                {
                    fields.Add((info.FieldType, info.Name));
                }
                
                tree.Add(nodeType + "/" + name, new NodeItem(name, nodeType, belongTo, fields.ToArray()));
                
                fields.Clear();
            }
            
            return tree;
        }
    }
}