using System.Collections.Generic;

namespace ET
{

    [ComponentOf]
    public class ActionComponent: Entity, IAwake, IDestroy, IUpdate
    {
        public Dictionary<string, TreeComponent> Actions;
        public string Current;
    }
}