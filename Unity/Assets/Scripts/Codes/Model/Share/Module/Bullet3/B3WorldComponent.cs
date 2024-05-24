using System.Collections.Generic;
using BulletSharp;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class B3WorldComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public DynamicsWorld World;
    }
}