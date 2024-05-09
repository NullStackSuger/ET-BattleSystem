using System;
using System.Collections.Generic;

namespace ET
{
    // 有个问题 : 当前是挂在Server里 所以Client调用这个应该会报空
    [ComponentOf(typeof(Scene))]
    public class NodeDispatcherComponent: Entity, IAwake, IDestroy, ILoad
    {
        [StaticField]
        public static NodeDispatcherComponent Instance;

        // key : NodeComponent Type
        // value : NodeHandler
        public Dictionary<Type, ANodeHandler> NodeHandlers = new ();
    }
}