using System.Collections.Generic;

namespace ET.Server
{
    /// <summary>
    /// 用于统计该Room(Scene)所有脏数据, 发送给客户端
    /// </summary>
    /// 该类不是单例 因为每个房间对应一个 不是整个服务器对应一个
    public class LSFComponent : Entity
    {
        // 记录整局的cmd
        public Dictionary<uint, Queue<LSFCmd>> AllCmds = new(1024);
        
        public Queue<LSFCmd> Sends = new(8);

        public Queue<LSFCmd> Receives = new(8);
        
        public uint CurrentFrame;
    }
}