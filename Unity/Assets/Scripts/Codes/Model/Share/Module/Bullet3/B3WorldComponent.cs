using System.Collections.Generic;
using BulletSharp;

namespace ET
{
    [ComponentOf(typeof(Scene))]
    public class B3WorldComponent : Entity, IAwake, IUpdate, IDestroy
    {
        public DynamicsWorld World;
        public Dictionary<CollisionObject, ContactResultCallback> Callbacks = new();
    }
}