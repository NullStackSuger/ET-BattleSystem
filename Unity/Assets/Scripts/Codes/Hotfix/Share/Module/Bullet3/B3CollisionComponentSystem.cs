using BulletSharp;

namespace ET
{
    public static class B3CollisionComponentSystem
    {
        public class B3CollisionComponentAwakeSystem : AwakeSystem<B3CollisionComponent, RigidBody, Unit>
        {
            protected override void Awake(B3CollisionComponent self, RigidBody body, Unit owner)
            {
                self.Body = body;
                self.Owner = owner;
            }
        }

        public class B3CollisionComponentDestroySystem : DestroySystem<B3CollisionComponent>
        {
            protected override void Destroy(B3CollisionComponent self)
            {
                self.Body.Dispose();
                self.Body = null;
                self.Owner = null;
            }
        }
    }
}