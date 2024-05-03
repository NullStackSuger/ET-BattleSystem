using System;
using System.IO;
using MongoDB.Bson.Serialization;
using NPBehave;
using Exception = NPBehave.Exception;

namespace ET
{

    public static class TreeFactory
    {
        private const string ClientPath = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Battle Tree Graph/Save";
        private const string ServerPath = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Battle Tree Graph/Save";
        private const string ViewPath   = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Battle Tree Graph/Save";
        
        /// <summary>
        /// 构建运行时行为树
        /// </summary>
        /// <param name="treeName">之后用enum表示, 编辑器构建时加一个enum元素</param>
        /// <returns></returns>
        public static NPBehave.Root Creat(string name, Unit unit)
        {
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
                
                var result = BsonSerializer.Deserialize<NPBehave.Root>(file);
                result.Unit = unit;
                
                Log.Info($"反序列化{name}.bytes 成功");

                return result;
            }
            catch (Exception e)
            {
                Log.Info(e.ToString());
                throw;
            }
        }
    }
}