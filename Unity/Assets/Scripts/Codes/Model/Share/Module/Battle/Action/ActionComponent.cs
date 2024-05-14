using System.Collections.Generic;

namespace ET
{

    [ComponentOf]
    public class ActionComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public List<int> Actions;
        public int Current;
        public ETCancellationToken CancellationToken;
    }
}