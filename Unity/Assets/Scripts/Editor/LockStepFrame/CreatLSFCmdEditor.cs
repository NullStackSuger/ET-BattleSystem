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
            List<(Type, string)> fields = new();
            foreach (Type type in this.GetAllCmds())
            {
                name = type.ToString().Split(".")[^1];
                name = name.Substring(3, name.Length - 3 - 3);
                
                // 遍历获取属性 : 反射获取属性
                fieldInfos = type.GetFields();
                foreach (FieldInfo info in fieldInfos)
                {
                    Debug.Log($"{info.FieldType} {info.Name}");
                    fields.Add((info.FieldType, info.Name));
                }
                
                tree.Add($"{name}", new LSFCmdItem(name, $"{name}Component", fields.ToArray()));
                fields.Clear();
            }
            
            return tree;
        }

        public class LSFCmdItem
        {
            [FoldoutGroup("节点信息")]
            public string Name;
            [FoldoutGroup("节点信息")][ShowInInspector]
            public Dictionary<Type, string> Fields = new();
            [FoldoutGroup("节点信息")][ShowInInspector]
            public string ConnectComponent;
            
            [LabelText("CmdPath")][ReadOnly]
            public string CmdPath = "";
            [LabelText("Server.CmdHandlerPath")][ReadOnly]
            public string ServerCmdHandlerPath = "";
            [LabelText("Client.CmdHandlerPath")][ReadOnly]
            public string ClientCmdHandlerPath = "";
            [LabelText("Server.ComponentHandlerPath")][ReadOnly]
            public string ServerComponentHandlerPath = "";
            [LabelText("Client.ComponentHandlerPath")][ReadOnly]
            public string ClientComponentHandlerPath = "";
            
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
                if (this.ServerCmdHandlerPath == "") this.ServerCmdHandlerPath = Setting.ServerCmdHandlerPath;
                if (this.ClientCmdHandlerPath == "") this.ClientCmdHandlerPath = Setting.ClientCmdHandlerPath;
                if (this.ServerComponentHandlerPath == "") this.ServerComponentHandlerPath = Setting.ServerComponentHandlerPath;
                if (this.ClientComponentHandlerPath == "") this.ClientComponentHandlerPath = Setting.ClientComponentHandlerPath;
                
                StringBuilder str = new();
                #region Cmd
                str.AppendLine("// 由Creat LSFCmd Editor生成");
                str.AppendLine("using ProtoBuf;");
                str.AppendLine("namespace ET");
                str.AppendLine("{");
                str.AppendLine("    [ProtoContract]");
                str.AppendLine("    [ProtobufBaseTypeRegister]");
                str.AppendLine($"    public class LSF{this.Name}Cmd: LSFCmd");
                str.AppendLine("    {");
                foreach (var field in this.Fields)
                {
                    str.AppendLine($"        public {field.Key} {field.Value};");
                }
                str.AppendLine("    }");
                str.AppendLine("}");

                SaveFile(this.CmdPath, $"LSF{this.Name}Cmd.cs", str.ToString());
                str.Clear();
                #endregion
                #region ServerComponentHandler
                str.AppendLine("// 由Creat LSFCmd Editor生成");
                str.AppendLine("namespace ET.Server");
                str.AppendLine("{");
                str.AppendLine($"    [LSFComponentHandler(typeof({this.ConnectComponent}))]");
                str.AppendLine("    [FriendOf(typeof(LSFComponent))]");
                str.AppendLine($"    public class LSF{this.Name}ComponentHandler : LSFComponentHandler");
                str.AppendLine("    {");
                str.AppendLine("        public override void TickStart(GameRoomComponent room, Entity component)");
                str.AppendLine("        {");
                str.AppendLine("            Log.Info(\"Server.TickStart\");");
                str.AppendLine("        }");
                str.AppendLine("        public override void Tick(GameRoomComponent room, Entity component)");
                str.AppendLine("        {");
                str.AppendLine("            Log.Info(\"Server.Tick\");");
                str.AppendLine("        }");
                str.AppendLine("        public override void TickEnd(GameRoomComponent room, Entity component)");
                str.AppendLine("        {");
                str.AppendLine("            Log.Info(\"Server.TickEnd\");");
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile(this.ServerComponentHandlerPath, $"LSF{this.Name}ComponentHandler.cs", str.ToString());
                str.Clear();
                #endregion
                #region ClientComponentHandler
                str.AppendLine("// 由Creat LSFCmd Editor生成");
                str.AppendLine("namespace ET.Client");
                str.AppendLine("{");
                str.AppendLine($"    [LSFComponentHandler(typeof({this.ConnectComponent}))]");
                str.AppendLine("    [FriendOf(typeof(LSFComponent))]");
                str.AppendLine($"    public class LSF{this.Name}ComponentHandler : LSFComponentHandler");
                str.AppendLine("    {");
                str.AppendLine("        public override void TickStart(GameRoomComponent room, Entity component, bool needSend)");
                str.AppendLine("        {");
                str.AppendLine("            Log.Info(\"Client.TickStart\");");
                str.AppendLine("        }");
                str.AppendLine("        public override void Tick(GameRoomComponent room, Entity component, bool needSend)");
                str.AppendLine("        {");
                str.AppendLine("            Log.Info(\"Client.Tick\");");
                str.AppendLine("        }");
                str.AppendLine("        public override void TickEnd(GameRoomComponent room, Entity component, bool needSend)");
                str.AppendLine("        {");
                str.AppendLine("            Log.Info(\"Client.TickEnd\");");
                str.AppendLine("        }");
                str.AppendLine("        public override bool Check(GameRoomComponent room, Entity component, LSFCmd cmd)");
                str.AppendLine("        {");
                str.AppendLine("            Log.Info(\"Client.Check\");");
                str.AppendLine("            return true;");
                str.AppendLine("        }");
                str.AppendLine("        public override void RollBack(GameRoomComponent room, Entity component, LSFCmd cmd)");
                str.AppendLine("        {");
                str.AppendLine("            Log.Info(\"Client.RollBack\");");
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile(this.ClientComponentHandlerPath, $"LSF{this.Name}ComponentHandler.cs", str.ToString());
                str.Clear();
                #endregion
                #region ServerCmdHandlerPath
                str.AppendLine("// 由Creat LSFCmd Editor生成");
                str.AppendLine("namespace ET.Server");
                str.AppendLine("{");
                str.AppendLine($"    [LSFCmdHandler(typeof (LSF{this.Name}Cmd))]");
                str.AppendLine($"    public class LSF{this.Name}CmdHandler: LSFCmdHandler");
                str.AppendLine("    {");
                str.AppendLine("        public override void Receive(Unit unit, LSFCmd cmd)");
                str.AppendLine("        {");
                str.AppendLine($"            var {Char.ToLower(this.Name[0]) + this.Name.Substring(1)}Cmd = cmd as LSF{this.Name}Cmd;");
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile(this.ServerCmdHandlerPath, $"LSF{this.Name}CmdHandler.cs", str.ToString());
                str.Clear();
                #endregion
                #region ClientCmdHandlerPath
                str.AppendLine("// 由Creat LSFCmd Editor生成");
                str.AppendLine("namespace ET.Client");
                str.AppendLine("{");
                str.AppendLine($"    [LSFCmdHandler(typeof (LSF{this.Name}Cmd))]");
                str.AppendLine($"    public class LSF{this.Name}CmdHandler: LSFCmdHandler");
                str.AppendLine("    {");
                str.AppendLine("        public override void Receive(Unit unit, LSFCmd cmd)");
                str.AppendLine("        {");
                str.AppendLine($"            var {Char.ToLower(this.Name[0]) + this.Name.Substring(1)}Cmd = cmd as LSF{this.Name}Cmd;");
                str.AppendLine("        }");
                str.AppendLine("    }");
                str.AppendLine("}");
                
                SaveFile(this.ClientCmdHandlerPath, $"LSF{this.Name}CmdHandler.cs", str.ToString());
                str.Clear();
                #endregion
            }
            
            [Button("Delete", 25), GUIColor(0.4f, 0.8f, 0)]
            public void Delete()
            {
                if (this.ServerCmdHandlerPath == "" || this.ClientCmdHandlerPath == "" || this.ServerComponentHandlerPath == "" || this.ClientComponentHandlerPath == "")
                    return;
                
                string path = $"{this.ServerCmdHandlerPath}/LSF{this.Name}CmdHandler.cs";
                if (File.Exists(path)) File.Delete(path);
                else Debug.LogWarning($"文件不存在: {path}");
                path = $"{this.ClientCmdHandlerPath}/LSF{this.Name}CmdHandler.cs";
                if (File.Exists(path)) File.Delete(path);
                else Debug.LogWarning($"文件不存在: {path}");
                path = $"{this.ServerComponentHandlerPath}/LSF{this.Name}ComponentHandler.cs";
                if (File.Exists(path)) File.Delete(path);
                else Debug.LogWarning($"文件不存在: {path}");
                path = $"{this.ClientComponentHandlerPath}/LSF{this.Name}ComponentHandler.cs";
                if (File.Exists(path)) File.Delete(path);
                else Debug.LogWarning($"文件不存在: {path}");
            }

            public LSFCmdItem()
            {
            }
            public LSFCmdItem(string name, string connectComponent, params (Type, string)[] fields)
            {
                this.Name = name;
                this.ConnectComponent = connectComponent;
                foreach (var field in fields)
                {
                    Fields.Add(field.Item1, field.Item2);
                }

                this.CmdPath = Setting.CmdPath;
                this.ServerCmdHandlerPath = Setting.ServerCmdHandlerPath;
                this.ClientCmdHandlerPath = Setting.ClientCmdHandlerPath;
                this.ServerComponentHandlerPath = Setting.ServerComponentHandlerPath;
                this.ClientComponentHandlerPath = Setting.ClientComponentHandlerPath;
            }
        }

        public class Setting
        {
            [LabelText("Cmd")] [FolderPath][StaticField][ShowInInspector]
            public static string CmdPath = "Assets/Scripts/Codes/Model/Share/Module/LockStepFrame/Cmds";
            [LabelText("Server.CmdHandler")] [FolderPath][StaticField][ShowInInspector]
            public static string ServerCmdHandlerPath = "Assets/Scripts/Codes/Hotfix/Server/Module/LockStepFrame/CmdHandlers";
            [LabelText("Client.CmdHandler")] [FolderPath][StaticField][ShowInInspector]
            public static string ClientCmdHandlerPath = "Assets/Scripts/Codes/Hotfix/Client/Module/LockStepFrame/CmdHandlers";
            [LabelText("Server.ComponentHandler")] [FolderPath][StaticField][ShowInInspector]
            public static string ServerComponentHandlerPath = "Assets/Scripts/Codes/Hotfix/Server/Module/LockStepFrame/ComponentHandlers";
            [LabelText("Client.ComponentHandler")] [FolderPath][StaticField][ShowInInspector]
            public static string ClientComponentHandlerPath = "Assets/Scripts/Codes/Hotfix/Client/Module/LockStepFrame/ComponentHandlers";
        }
    }
}