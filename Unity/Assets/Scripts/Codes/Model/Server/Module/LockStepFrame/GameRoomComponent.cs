using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class GameRoomComponent : Entity, IAwake, IDestroy, IUpdate
    {
        /// <summary>
        /// 需要Tick同步的Units
        /// </summary>
        public List<Unit> Syncs = new();

        public SortedDictionary<uint, SortedSet<LSFCmd>> AllCmds = new();

        public SortedDictionary<uint, SortedSet<LSFCmd>> Sends = new();

        public SortedDictionary<uint, SortedSet<LSFCmd>> Receives = new();
        
        public uint Frame;

        public readonly uint TickRateFrame = 30;

        public bool IsStart;
    }
}