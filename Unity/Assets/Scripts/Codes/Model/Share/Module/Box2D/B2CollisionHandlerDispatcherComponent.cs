using System.Collections.Generic;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class B2CollisionHandlerDispatcherComponent : Entity, IAwake
    {
        [StaticField]
        public static B2CollisionHandlerDispatcherComponent Instance;
        public Dictionary<string, AB2CollisionHandler> Handlers;
    }
}