using System.Collections.Generic;

namespace ET.Client
{
    /// <summary>
    /// 用于统计该Unit所有操作, 发送给服务端
    /// 接收服务端值, 处理冲突
    /// </summary>
    public class LSFComponent : Entity, IAwake, IUpdate, IDestroy
    {
        // 这些Queue最好都用字典包裹一下, 能更灵活
        
        public Dictionary<uint, Queue<LSFCmd>> Sends = new(8);

        public Dictionary<uint, Queue<LSFCmd>> Receives = new(8);
        
        // 存储 接收到服务器回包帧号 - 当前帧号
        // 这里认为Buffer = 1
        // Length = RTT + Buffer
        public Dictionary<uint, Queue<LSFCmd>> Buffer = new();

        /// <summary>
        /// 是否在线
        /// </summary>
        public bool IsOnline = true;
        
        public uint CurrentFrame;

        /*/// <summary>
        /// 客户端来模拟服务端tick
        /// </summary>
        public uint ServerCurrentFrame;

        public uint HalfRTT;*/

        /// <summary>
        /// 客户端最多领先服务端帧数
        /// 客户端 - 服务端 > MaxAheadFrame 时认为掉线了
        /// </summary>
        [StaticField]
        public static readonly uint MaxAheadFrame = 10;
    }
}