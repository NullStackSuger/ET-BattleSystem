using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class GameRoomComponent : Entity, IAwake, IUpdate
    {
        /// <summary>
        /// 客户端最大领先帧数
        /// </summary>
        [StaticField]
        public static readonly uint MaxAhead = 10;
        
        public SortedDictionary<uint, SortedSet<LSFCmd>> Sends = new();

        public SortedDictionary<uint, SortedSet<LSFCmd>> Receives = new();
                
        public uint Frame;

        /// <summary>
        /// 客户端操控的Unit
        /// </summary>
        public Unit MainPlayer { get; set; }

        /// <summary>
        /// 客户端当前领先服务端帧数
        /// </summary>
        public float CurrentAhead
        {
            get
            {
                if (this.Receives.Count <= 0)
                    return this.Frame - 0;
                else
                    return this.Frame - this.Receives.Last().Value.First().Frame + 0;
            }
        }

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
        
        /// <summary>
        /// 客户端每秒Tick帧数
        /// </summary>
        public uint TickRateFrame
        {
            get
            {
                return (uint)(0.33 + (this.TargetAhead - this.CurrentAhead));
            }
        }
    }
}