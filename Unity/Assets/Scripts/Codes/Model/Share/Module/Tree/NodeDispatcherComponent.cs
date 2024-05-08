using System;
using System.Collections.Generic;

namespace ET
{
    public class NodeDispatcherComponent : Entity, IAwake, IDestroy
    {
        [StaticField]
        public static NodeDispatcherComponent Instance;
        
        public Dictionary<Type, NodeRun> Nodes = new();
    }
}