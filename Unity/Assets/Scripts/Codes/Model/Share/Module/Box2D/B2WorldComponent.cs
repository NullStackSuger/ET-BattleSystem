using System.Collections.Generic;
using Box2DSharp.Dynamics;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class B2WorldComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public World World;

        public B2ContactListener Listener;
    }
}