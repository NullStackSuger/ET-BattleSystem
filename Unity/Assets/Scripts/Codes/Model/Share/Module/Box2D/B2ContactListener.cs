using System.Collections.Generic;
using Box2DSharp.Collision.Collider;
using Box2DSharp.Dynamics;
using Box2DSharp.Dynamics.Contacts;

namespace ET
{
    [FriendOf(typeof(B2CollisionHandlerDispatcherComponent))]
    [FriendOfAttribute(typeof(ET.B2CollisionComponent))]
    public class B2ContactListener : IContactListener, IUpdate
    {
        private List<(Unit, Unit)> CollisionRecorder = new();
        
        public void BeginContact(Contact contact)
        {
            //这里获取的是碰撞实体，比如诺克Q技能的碰撞体Unit，这里获取的就是它
            Unit unitA = (Unit)contact.FixtureA.UserData;
            Unit unitB = (Unit)contact.FixtureB.UserData;

            if (unitA.IsDisposed || unitB.IsDisposed) return;

            CollisionRecorder.Add((unitA, unitB));
            CollisionRecorder.Add((unitB, unitA));

            var handlerA = GetHandler(unitA);
            var handlerB = GetHandler(unitB);
            handlerA.CollisionStart(unitA, unitB);
            handlerB.CollisionStart(unitB, unitA);
        }

        public void EndContact(Contact contact)
        {
            Unit unitA = (Unit)contact.FixtureA.UserData;
            Unit unitB = (Unit)contact.FixtureB.UserData;
            
            CollisionRecorder.Remove((unitA, unitB));
            CollisionRecorder.Remove((unitB, unitA));

            if (unitA.IsDisposed || unitB.IsDisposed) return;

            var handlerA = GetHandler(unitA);
            var handlerB = GetHandler(unitB);
            handlerA.CollisionStart(unitA, unitB);
            handlerB.CollisionStart(unitB, unitA);
        }

        public void PreSolve(Contact contact, in Manifold oldManifold)
        {

        }

        public void PostSolve(Contact contact, in ContactImpulse impulse)
        {

        }

        // 因为没继承Entity, 这里要由外部World调用
        public void Update()
        {
            for (int i = this.CollisionRecorder.Count - 1; i >= 0; --i)
            {
                Unit unitA = this.CollisionRecorder[i].Item1;
                Unit unitB = this.CollisionRecorder[i].Item2;

                if (unitA == null || unitB == null || unitA.IsDisposed || unitB.IsDisposed)
                {
                    CollisionRecorder.RemoveAt(i);
                    continue;
                }

                var handlerA = GetHandler(unitA);
                var handlerB = GetHandler(unitB);
                handlerA.CollisionSustain(unitA ,unitB);
                handlerB.CollisionSustain(unitA, unitB);
            }
        }

        private AB2CollisionHandler GetHandler(Unit unit)
        {
            if (B2CollisionHandlerDispatcherComponent.Instance.Handlers.TryGetValue(
                    unit.GetComponent<B2CollisionComponent>().HandlerName,
                    out AB2CollisionHandler handler))
            {
                return handler;
            }

            Log.Error($"未获取到AB2CollisionHandler");
            return null;
        }
    }
}