using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Unit))]
    public class BuffComponent: Entity, IAwake, IDestroy, ITransfer
    {
        public Dictionary<int, Buff> Buffs = new();
    }
}