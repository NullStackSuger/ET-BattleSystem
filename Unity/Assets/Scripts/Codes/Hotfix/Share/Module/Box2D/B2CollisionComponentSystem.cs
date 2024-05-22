using Box2DSharp.Dynamics;

namespace ET
{
    public static class B2CollisionComponentSystem
    {
        public class B2CollisionComponentAwakeSystem: AwakeSystem<B2CollisionComponent, string, Body, Unit>
        {
            protected override void Awake(B2CollisionComponent self, string handlerName, Body body, Unit owner)
            {
                self.HandlerName = handlerName;
                self.Body = body;
                self.Owner = owner;
            }
        }
        
        public class B2CollisionComponentDestroySystem : DestroySystem<B2CollisionComponent>
        {
            protected override void Destroy(B2CollisionComponent self)
            {
                self.HandlerName = "";
                self.Body = null;
                self.Owner = null;
            }
        }
    }
}