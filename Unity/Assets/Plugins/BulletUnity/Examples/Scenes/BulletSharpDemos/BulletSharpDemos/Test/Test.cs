using System;
using BulletSharp;
using BulletSharp.Math;
using BulletSharp.SoftBody;
using BulletSharpExamples;
using BulletUnity;
using CollisionInterfaceDemo;
using UnityEngine;
using CollisionFlags = BulletSharp.CollisionFlags;
using Graphics = BulletSharpExamples.Graphics;
using Vector3 = BulletSharp.Math.Vector3;

public class Test : MonoBehaviour
{
    private DynamicsWorld world;
    private RigidBody fall;

    private void Start()
    {
        World0();
    }

    private void World0()
    {
        Vector3 worldAabbMin = Vector3.One * -10000;
        Vector3 worldAabbMax = Vector3.One * 10000;
        var broadPhase = new AxisSweep3(worldAabbMin, worldAabbMax, 1024);
        var collisionConf = new DefaultCollisionConfiguration();
        var dispatcher = new CollisionDispatcher(collisionConf);
        var solver = new SequentialImpulseConstraintSolver();
        world = new DiscreteDynamicsWorld(dispatcher, broadPhase, solver, collisionConf);
        world.Gravity = new Vector3(0, -10, 0);
        
        // Add Ground
        CollisionShape groundShape = new StaticPlaneShape(new Vector3(0, 1, 0), 1);
        DefaultMotionState groundMotionState = new(Matrix.Translation(0,0,0));
        RigidBodyConstructionInfo groundBodyInfo = new(0, groundMotionState, groundShape, Vector3.Zero);
        RigidBody ground = new RigidBody(groundBodyInfo);
        world.AddRigidBody(ground);
        
        // Add Ball
        CollisionShape fallShape = new SphereShape(1);
        DefaultMotionState fallMotionState = new(Matrix.Translation(0,50,0));
        Vector3 fallInertia = fallShape.CalculateLocalInertia(1);
        RigidBodyConstructionInfo fallBodyInfo = new(1, fallMotionState, fallShape, fallInertia);
        fall = new RigidBody(fallBodyInfo);
        world.AddRigidBody(fall);
    }

    private void World1()
    {
        var CollisionConf = new DefaultCollisionConfiguration();
        var Dispatcher = new CollisionDispatcher(CollisionConf);
        var Broadphase = new DbvtBroadphase();
        world = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConf);
        world.Gravity = new Vector3(0, -10, 0);
        
        BoxShape groundShape = new BoxShape(50, 1, 50);
        CollisionObject ground = LocalCreateRigidBody(100, Matrix.Identity, groundShape);
        ground.WorldTransform = Matrix.Translation(10, 500, 10);
        Debug.Log(ground.WorldTransform.GetOrientation());
        ground.UserObject = "Ground";
    }

    private void FixedUpdate()
    {
        world.StepSimulation(Time.fixedDeltaTime);
        fall.GetWorldTransform(out Matrix transform);
        Debug.Log(transform);
    }

    RigidBody LocalCreateRigidBody(float mass, Matrix startTransform, CollisionShape shape, bool isKinematic = false)
    {
        // 质量 != 0 为动态 否则静态
        bool isDynamic = (mass != 0.0f);

        Vector3 localInertia = Vector3.Zero;
        if (isDynamic)
            shape.CalculateLocalInertia(mass, out localInertia);

        //using motionstate is recommended, it provides interpolation capabilities, and only synchronizes 'active' objects
        DefaultMotionState myMotionState = new DefaultMotionState(startTransform);

        RigidBodyConstructionInfo rbInfo = new RigidBodyConstructionInfo(mass, myMotionState, shape, localInertia);
        RigidBody body = new RigidBody(rbInfo);
        if (isKinematic)
        {
            body.CollisionFlags = body.CollisionFlags | CollisionFlags.KinematicObject;
            body.ActivationState = ActivationState.DisableDeactivation;
        }
        rbInfo.Dispose();

        world.AddRigidBody(body);

        return body;
    }
}