using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class GameRoomComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public bool IsStart;
        
        public List<LSFComponent> Syncs = new();
        
        public uint Frame;

        public readonly uint TickRateFrame = 30;
    }
}