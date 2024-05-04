using System.Collections.Generic;

namespace ET.Server
{

    [ComponentOf(typeof(Unit))]
    public class CastComponent: Entity, IAwake, IDestroy, IUpdate, ITransfer
    {
        public List<Cast> Casts = new();
    }
}