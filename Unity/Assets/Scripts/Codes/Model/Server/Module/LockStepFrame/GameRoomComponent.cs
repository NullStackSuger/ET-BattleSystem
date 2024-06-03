using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class GameRoomComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public List<LSFComponent> Syncs = new();

        public Dictionary<uint, Queue<LSFCmd>> AllCmds = new();
        
        public uint Frame;

        public readonly uint TickRateFrame = 30;

        public bool IsStart;
    }
}