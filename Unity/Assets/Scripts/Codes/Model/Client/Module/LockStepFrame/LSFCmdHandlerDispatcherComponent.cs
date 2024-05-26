using System;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class LSFCmdHandlerDispatcherComponent : Entity, IAwake, IDestroy
    {
        [StaticField]
        public static LSFCmdHandlerDispatcherComponent Instance;

        public Dictionary<Type, LSFCmdHandler> Handlers;
    }
}