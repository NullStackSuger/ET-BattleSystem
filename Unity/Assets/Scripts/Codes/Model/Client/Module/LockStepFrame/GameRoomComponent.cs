using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    [FriendOfAttribute(typeof(ET.Client.LSFComponent))]
    public class GameRoomComponent : Entity, IUpdate, IDestroy
    {
        /// <summary>
        /// 客户端最大领先帧数
        /// </summary>
        [StaticField]
        public static readonly uint MaxAhead = 10;
        
        public uint Frame;
        
        /// <summary>
        /// 客户端操控的Unit
        /// </summary>
        public Unit MainPlayer;
        
        /// <summary>
        /// 客户端当前领先服务端帧数
        /// </summary>
        public float CurrentAhead
        {
            get
            {
                return this.Frame - this.MainPlayer.GetComponent<LSFComponent>().Receives.Last().Value.Peek().Frame + 0;
            }
        }

        /// <summary>
        /// 客户端应该领先服务端帧数
        /// 在外部传测试消息来计算
        /// </summary>
        /// 外部做个定时器, 每隔多少s传递一下, 赋值TargetAhead
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