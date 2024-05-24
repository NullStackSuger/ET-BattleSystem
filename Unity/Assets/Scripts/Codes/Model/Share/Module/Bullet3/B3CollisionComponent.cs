using BulletSharp;

namespace ET
{
    [ChildOf(typeof(B3WorldComponent))]
    public class B3CollisionComponent : Entity, IAwake<RigidBody, Unit>, IDestroy
    {
        public RigidBody Body;
        public Unit Owner; // 所属玩家
    }
}