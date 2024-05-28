using System;
using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Scene))]
    public class LSFComponentHandlerDispatcherComponent :  Entity, IAwake, IDestroy
    {
        [StaticField]
        public static LSFComponentHandlerDispatcherComponent Instance;

        public Dictionary<Type, LSFComponentHandler> Handlers = new();
    }
}