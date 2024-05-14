using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class BuffComponent: Entity, IAwake, IDestroy, IUpdate, ITransfer
    {
        public Dictionary<int, Buff> Buffs = new();
    }
}