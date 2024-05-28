using System;
using System.Collections.Generic;
using System.Linq;
using BulletSharp;
using BulletSharp.Math;
using Sirenix.OdinInspector;

namespace ET
{
    [FriendOf(typeof(B3WorldComponent))]
    [FriendOfAttribute(typeof(ET.B3CollisionComponent))]
    public static class B3WorldComponentSystem
    {
        public class B3WorldAwakeComponentSystem : AwakeSystem<B3WorldComponent>
        {
            protected override void Awake(B3WorldComponent self)
            {
                var CollisionConf = new DefaultCollisionConfiguration();
                var Dispatcher = new CollisionDispatcher(CollisionConf);
                var BroadPhase = new DbvtBroadphase();
                self.World = new DiscreteDynamicsWorld(Dispatcher, BroadPhase, null, CollisionConf);
                self.World.Gravity = new Vector3(0, -9.8f, 0);
            }
        }

        public class B3WorldUpdateComponentSystem : UpdateSystem<B3WorldComponent>
        {
            protected override void Update(B3WorldComponent self)
            {
                self.World.StepSimulation(1.0f / TimeHelper.DeltaTime());
                
                Log.Warning(TimeHelper.DeltaTime());
                /*if (self.World.CollisionObjectArray.Count > 0)
                    Log.Warning(self.World.CollisionObjectArray[0].WorldTransform.ToString());*/
                    
                foreach (var pair in self.Callbacks)
                {
                    self.World.ContactTest(pair.Key, pair.Value);
                }
            }
        }
        
        [FriendOf(typeof(B3CollisionComponent))]
        public class B3WorldDestroyComponentSystem : DestroySystem<B3WorldComponent>
        {
            protected override void Destroy(B3WorldComponent self)
            {
                self.World.Dispose();
                self.World = null;
            }
        }

        public static B3CollisionComponent AddBody(this B3WorldComponent self, RigidBodyConstructionInfo info, Unit owner, ContactResultCallback  callback = null)
        {
            RigidBody body = new RigidBody(info);
            self.World.AddRigidBody(body);
            if(callback != null) self.Callbacks.Add(body, callback);
            var collision = self.AddChild<B3CollisionComponent, RigidBody, Unit>(body, owner);
            return collision;
        }

        public static void RemoveBody(this B3WorldComponent self, long collisionId)
        {
            var collision = self.GetChild<B3CollisionComponent>(collisionId);
            if (self.Callbacks.ContainsKey(collision.Body))
                self.Callbacks.Remove(collision.Body);
            collision.Body.Dispose();
            self.World.RemoveRigidBody(collision.Body);
            self.RemoveChild(collisionId);
        }
    }
}