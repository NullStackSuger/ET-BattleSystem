using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// 用于统计该Unit所有操作, 发送给服务端
    /// 接收服务端值, 处理冲突
    /// </summary>
    public class LSFComponent : Entity
    {
        public Queue<LSFCmd> Sends = new(8);

        public Queue<LSFCmd> Receives = new(8);
        
        // 存储 接收到服务器回包帧号 - 当前帧号
        // Length = RTT + Buffer
        public Dictionary<uint, Queue<LSFCmd>> Buffer = new();
        
        public uint CurrentFrame;
    }
}