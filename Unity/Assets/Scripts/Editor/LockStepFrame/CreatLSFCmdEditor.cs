using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace ET
{
    public class CreatLSFCmdEditor : OdinMenuEditorWindow
    {
        [MenuItem("ET/Creat LSFCmd Editor")]
        private static void OpenWindow()
        {
            GetWindow<CreatLSFCmdEditor>().Show();
        }
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;
            tree.Add("Setting", new Setting());
            tree.Add("Creat New", new LSFCmdItem());

            string name = "";
            FieldInfo[] fieldInfos = null;
            Dictionary<string, Type> fields = new();
            foreach (Type type in this.GetAllCmds())
            {
                name = type.ToString().Split(".")[^1];
                name = name.Substring(3, name.Length - 3 - 3);
                
                // 遍历获取属性 : 反射获取属性
                fieldInfos = type.GetFields();
                Type baseType = type.BaseType;
                foreach (FieldInfo info in fieldInfos)
                {
                    if (baseType.GetField(info.Name) != null) continue;
                    fields.Add(info.Name, info.FieldType);
                }
                
                tree.Add($"{name}", new LSFCmdItem(name, $"{name}Component", fields));
                fields.Clear();
            }
            
            return tree;
        }

        public class LSFCmdItem
        {
            [FoldoutGroup("节点信息")]
            public string Name;
            [FoldoutGroup("节点信息")][ShowInInspector]
            public Dictionary<string, Type> Fields = new();
            [FoldoutGroup("节点信息")][ShowInInspector]
            public string ConnectComponent;
            
            [LabelText("CmdPath")][ReadOnly]
            public string CmdPath = "";
            [LabelText("Server HandlerPath")][ReadOnly]
            public string ServerHandlerPath = "";
            [LabelText("Client HandlerPath")][ReadOnly]
            public string ClientHandlerPath = "";
            
            private void SaveFile(string path, string name, string context)
            {
                if (File.Exists(path + "/" + name)) File.Delete($"{path}/{name}");

                using FileStream fs = File.Create($"{path}/{name}");
                fs.Write(Encoding.UTF8.GetBytes(context));
            }
            
            [Button("Apply", 25), GUIColor(0.4f, 0.8f, 1)]
            public void Apply()
            {
                if (this.CmdPath == "") this.CmdPath = Setting.CmdPath;
                if (this.ServerHandlerPath == "") this.ServerHandlerPath = Setting.ServerHandlerPath;
                if (this.ClientHandlerPath == "") this.ClientHandlerPath = Setting.ClientHandlerPath;
                
                StringBuilder str = new();
                
                #region Cmd
                str.AppendLine("// 由Creat LSFCmd Editor生成");
                str.AppendLine("using ProtoBuf;");
                str.AppendLine("using System.Collections.Generic;");
                str.AppendLine("namespace ET");
                str.AppendLine("{");
                str.AppendLine("    [ProtoContract]");
                str.AppendLine("    [ProtobufBaseTypeRegister]");
                str.AppendLine($"    public class LSF{this.Name}Cmd: LSFCmd");
                str.AppendLine("    {");
                int index = 1;
                foreach (var field in this.Fields)
                {
                    str.AppendLine($"        [ProtoMember({index++})]");
                    str.AppendLine($"        public {field.Value} {field.Key};");
                }
                str.AppendLine("    }");
                str.AppendLine("}");

                SaveFile(this.CmdPath, $"LSF{this.Name}Cmd.cs", str.ToString());
                str.Clear();
                #endregion

                #region Server LSFHandler
                str.AppendLine("// 由Creat LSFCmd Editor生成");
                str.AppendLine("namespace ET.Server");
                str.AppendLine("{");
                str.AppendLine($"    [LSFHandler(typeof({this.ConnectComponent}), typeof(LSF{this.Name}Cmd))]");
                str.AppendLine("    [FriendOf(typeof(GameRoomComponent))]");
                str.AppendLine($"    public class LSF{this.Name}Handler: LSFHandler<{this.ConnectComponent}, LSF{this.Name}Cmd>");
                str.AppendLine("    {");
                str.AppendLine($"        public override void TickStart(GameRoomComponent room, {this.ConnectComponent} component)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine($"        public override void Tick(GameRoomComponent room, {this.ConnectComponent} component)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine($"        public override void TickEnd(GameRoomComponent room, {this.ConnectComponent} component)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine();
                str.AppendLine($"        public override void Receive(Unit unit, LSF{this.Name}Cmd cmd)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile(this.ServerHandlerPath, $"LSF{this.Name}Handler.cs", str.ToString());
                str.Clear();
                #endregion
                
                #region Client LSFHandler
                str.AppendLine("// 由Creat LSFCmd Editor生成");
                str.AppendLine("namespace ET.Client");
                str.AppendLine("{");
                str.AppendLine($"    [LSFHandler(typeof({this.ConnectComponent}), typeof(LSF{this.Name}Cmd))]");
                str.AppendLine("    [FriendOf(typeof(GameRoomComponent))]");
                str.AppendLine($"    public class LSF{this.Name}Handler: LSFHandler<{this.ConnectComponent}, LSF{this.Name}Cmd>");
                str.AppendLine("    {");
                str.AppendLine($"        public override void TickStart(GameRoomComponent room, {this.ConnectComponent} component, bool inRollBack)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine($"        public override void Tick(GameRoomComponent room, {this.ConnectComponent} component, bool inRollBack)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine($"        public override void TickEnd(GameRoomComponent room, {this.ConnectComponent} component, bool inRollBack)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine();
                str.AppendLine($"        public override void Receive(Unit unit, LSF{this.Name}Cmd cmd)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine($"        public override bool Check(GameRoomComponent room, {this.ConnectComponent} component, LSF{this.Name}Cmd cmd)");
                str.AppendLine("        {");
                str.AppendLine("            return true;");
                str.AppendLine("        }");
                str.AppendLine($"        public override void RollBack(GameRoomComponent room, {this.ConnectComponent} component, LSF{this.Name}Cmd cmd)");
                str.AppendLine("        {");
                str.AppendLine();
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile(this.ClientHandlerPath, $"LSF{this.Name}Handler.cs", str.ToString());
                str.Clear();
                #endregion
            }
            
            [Button("Delete", 25), GUIColor(0.4f, 0.8f, 0)]
            public void Delete()
            {
                if (this.CmdPath == "" || this.ServerHandlerPath == "" || this.ClientHandlerPath == "") return;

                string path = "";
                
                path = $"{this.CmdPath}/LSF{this.Name}Cmd.cs";
                if (File.Exists(path)) File.Delete(path);
                else Debug.LogWarning($"文件不存在: {path}");
                path = $"{this.ServerHandlerPath}/LSF{this.Name}CmdHandler.cs";
                if (File.Exists(path)) File.Delete(path);
                else Debug.LogWarning($"文件不存在: {path}");
                path = $"{this.ClientHandlerPath}/LSF{this.Name}CmdHandler.cs";
                if (File.Exists(path)) File.Delete(path);
                else Debug.LogWarning($"文件不存在: {path}");
            }

            public LSFCmdItem()
            {
            }
            public LSFCmdItem(string name, string connectComponent, params (string, Type)[] fields)
            {
                this.Name = name;
                this.ConnectComponent = connectComponent;
                foreach (var field in fields)
                {
                    Fields.Add(field.Item1, field.Item2);
                }

                this.CmdPath = Setting.CmdPath;
                this.ServerHandlerPath = Setting.ServerHandlerPath;
                this.ClientHandlerPath = Setting.ClientHandlerPath;
            }

            public LSFCmdItem(string name, string connectComponent, Dictionary<string, Type> fields)
            {
                this.Name = name;
                this.ConnectComponent = connectComponent;
                foreach (var field in fields)
                {
                    Fields.Add(field.Key, field.Value);
                }

                this.CmdPath = Setting.CmdPath;
                this.ServerHandlerPath = Setting.ServerHandlerPath;
                this.ClientHandlerPath = Setting.ClientHandlerPath;
            }
        }

        public class Setting
        {
            [LabelText("Cmd")] [FolderPath][StaticField][ShowInInspector]
            public static string CmdPath = "Assets/Scripts/Codes/Model/Share/Module/LockStepFrame/Cmds";
            [LabelText("Server Handler")] [FolderPath][StaticField][ShowInInspector]
            public static string ServerHandlerPath = "Assets/Scripts/Codes/Hotfix/Server/Module/LockStepFrame/LSFHandler";
            [LabelText("Client Handler")] [FolderPath][StaticField][ShowInInspector]
            public static string ClientHandlerPath = "Assets/Scripts/Codes/Hotfix/Client/Module/LockStepFrame/LSFHandler";
        }
    }
}