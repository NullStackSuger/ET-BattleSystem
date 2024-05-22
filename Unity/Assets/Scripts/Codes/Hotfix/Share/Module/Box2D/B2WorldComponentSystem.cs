using System.Numerics;
using Box2DSharp.Dynamics;

namespace ET
{
    [FriendOf(typeof(B2WorldComponent))]
    [FriendOf(typeof(B2CollisionComponent))]
    public static class B2WorldComponentSystem
    {
        public class B2WorldAwakeSystem : AwakeSystem<B2WorldComponent>
        {
            protected override void Awake(B2WorldComponent self)
            {
                self.World = new(new Vector2(0, -9.8f));
                self.Listener = new();
                self.World.SetContactListener(self.Listener);
            }
        }

        public class B2WorldUpdateSystem : UpdateSystem<B2WorldComponent>
        {
            protected override void Update(B2WorldComponent self)
            {
                self.Listener.Update();
                self.World?.Step(TimeHelper.ServerFrameTime(), 10, 10);
            }
        }
        
        public class B2WorldDestroySystem : DestroySystem<B2WorldComponent>
        {
            protected override void Destroy(B2WorldComponent self)
            {
                if (self.World.BodyCount > 0)
                    Log.Warning($"存在{self.World.BodyCount}个未销毁刚体");
                
                self.Listener = null;
                self.World = null;
            }
        }
        
        /// <summary>
        /// 为unit添加刚体body
        /// </summary>
        public static void AddBody(this B2WorldComponent self, Unit unit, string handlerName, BodyDef bodyDef, FixtureDef fixtureDef, Unit owner)
        {
            // Add Body To World
            Body body = self.World.CreateBody(bodyDef);
            body.CreateFixture(fixtureDef);
            // Add Component
            unit.AddComponent<B2CollisionComponent, string, Body, Unit>(handlerName, body, owner);
        }

        public static void RemoveBody(this B2WorldComponent self, Unit unit)
        {
            B2CollisionComponent collisionComponent = unit.GetComponent<B2CollisionComponent>();
            // Remove Body From World
            self.World.DestroyBody(collisionComponent.Body);
            // Remove Component
            unit.RemoveComponent<B2CollisionComponent>();
        }
    }
}