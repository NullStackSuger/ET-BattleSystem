using System.IO;
using MongoDB.Bson.Serialization;
using NPBehave;

namespace ET.Server
{
    public static class NP_TreeFactory
    {
        public static Root Creat(string path)
        {
            try
            {
                byte[] file = File.ReadAllBytes($"{path}.bytes");
                if (file.Length == 0) Log.Info("没有读取到文件");
                Root root = BsonSerializer.Deserialize<Root>(file);
                
                Log.Info($"反序列化 {path}.bytes 成功");
                return root;
            }
            catch (Exception e)
            {
                Log.Info(e.ToString());
                throw;
            }
        }
    }
}