using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class GameRoomComponent : Entity, IAwake, IDestroy, IUpdate
    {
        /// <summary>
        /// 客户端最大领先帧数
        /// </summary>
        [StaticField]
        public static readonly uint MaxAhead = 10;

        public MultiDictionary<uint, Type, LSFCmd> AllCmds = new();
        
        public SortedDictionary<uint, SortedSet<LSFCmd>> Sends = new();

        public SortedDictionary<uint, SortedSet<LSFCmd>> Receives = new();
                
        public uint Frame;

        /// <summary>
        /// 客户端操控的Unit
        /// </summary>
        public Unit MainPlayer { get; set; }
        
        /// <summary>
        /// 最后一次接收的帧数
        /// </summary>
        public uint LastReceiveFrame;

        /// <summary>
        /// 客户端应该领先服务端帧数
        /// 由M2C_FrameRes确定
        /// </summary>
        public uint TargetAhead
        {
            set
            {
                this.InnerTargetAhead = Math.Min(MaxAhead, value);
            }
            get
            {
                return InnerTargetAhead;
            }
        }
        private uint InnerTargetAhead = 5;

        public float TickRate = 0.33f;
    }
}