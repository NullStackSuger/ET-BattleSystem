using System.Collections.Generic;

namespace ET.Server
{

    [ComponentOf]
    public class ActionComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public List<int> Actions = new();
        public int Current = int.MinValue;
        public ETCancellationToken CancellationToken = new();
    }
}