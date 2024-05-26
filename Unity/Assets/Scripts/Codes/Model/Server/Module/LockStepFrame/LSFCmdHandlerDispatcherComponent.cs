using System;
using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Scene))]
    public class LSFCmdHandlerDispatcherComponent : Entity, IAwake, IDestroy
    {
        [StaticField]
        public static LSFCmdHandlerDispatcherComponent Instance;

        public Dictionary<Type, LSFCmdHandler> Handlers;
    }
}