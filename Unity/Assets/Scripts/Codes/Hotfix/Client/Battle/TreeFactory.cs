using System;
using System.IO;
using MongoDB.Bson.Serialization;
using NPBehave;
using Exception = NPBehave.Exception;

namespace ET.Client
{

    public static class TreeFactory
    {
        private const string FilePath = "D:/ToolSoft/U3D/Project/ETs/ET-BattleSystem/Unity/Assets/Scripts/Editor/Battle Tree Graph/Save";
        
        /// <summary>
        /// 构建运行时行为树
        /// </summary>
        /// <param name="treeName">之后用enum表示, 编辑器构建时加一个enum元素</param>
        /// <returns></returns>
        public static NPBehave.Root BuildTree(string name)
        {
            try
            {
                byte[] file = File.ReadAllBytes($"{FilePath}/{name}.bytes");
                if (file.Length == 0) Log.Info("没有读取到文件");
                var result = BsonSerializer.Deserialize<NPBehave.Root>(file);
                
                Log.Info($"反序列化 {FilePath}/{name}.bytes 成功");

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