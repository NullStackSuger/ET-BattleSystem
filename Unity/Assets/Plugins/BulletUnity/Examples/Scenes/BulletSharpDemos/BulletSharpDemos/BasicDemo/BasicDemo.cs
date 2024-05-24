using System;
using BulletSharp;
using BulletSharp.Math;
using DemoFramework;
using BulletSharpExamples;
using BulletUnity;
using Sirenix.OdinInspector;
using UnityEngine;
using Vector3 = BulletSharp.Math.Vector3;

namespace BasicDemo
{
    public class BasicDemo : Demo
    {
        Vector3 eye = new Vector3(30, 20, 10);
        Vector3 target = new Vector3(0, 5, -4);

        // create 125 (5x5x5) dynamic objects
        const int ArraySizeX = 5, ArraySizeY = 5, ArraySizeZ = 5;

        // scaling of the objects (0.1 = 20 centimeter boxes )
        const float StartPosX = -5;
        const float StartPosY = -5;
        const float StartPosZ = -3;

        [ShowInInspector]
        private RigidBody fall;

        protected override void OnInitialize()
        {
            // 设置摄像机从eye看向target
            Freelook.SetEyeTarget(eye, target);

            Graphics.SetFormText("BulletSharp - Basic Demo");
        }

        protected override void OnInitializePhysics()
        {
            // collision configuration contains default setup for memory, collision setup
            // 碰撞, 内存 设置信息
            CollisionConf = new DefaultCollisionConfiguration();
            Dispatcher = new CollisionDispatcher(CollisionConf);

            Broadphase = new DbvtBroadphase();

            // 定义世界属性
            World = new DiscreteDynamicsWorld(Dispatcher, Broadphase, null, CollisionConf);
            // 定义重力G
            World.Gravity = new Vector3(0, -10, 0);

            // create the ground
            // 创建地面
            // 设置缩放
            /*BoxShape groundShape = new BoxShape(50, 1, 50);
            // 初始化多面体特点?
            //groundShape.InitializePolyhedralFeatures();
            // 同时设置位置缩放
            //CollisionShape groundShape = new StaticPlaneShape(new Vector3(0,1,0), 50);

            // 应该是处理碰撞的列表
            CollisionShapes.Add(groundShape);
            CollisionObject ground = LocalCreateRigidBody(0, Matrix.Identity, groundShape);
            ground.UserObject = "Ground";*/
            
            
            const float mass = 1.0f;
            
            // Creat Fall
            CollisionShape fallShape = new SphereShape(1);
            CollisionShapes.Add(fallShape);
            DefaultMotionState fallMotionState = new(Matrix.Translation(0,50,0));
            Vector3 fallInertia = fallShape.CalculateLocalInertia(mass);
            RigidBodyConstructionInfo fallBodyInfo = new(mass, fallMotionState, fallShape, fallInertia);
            fall = new RigidBody(fallBodyInfo);
            World.AddRigidBody(fall);
            
            /*// create a few dynamic rigidbodies
            BoxShape colShape = new BoxShape(1);
            CollisionShapes.Add(colShape);
            Vector3 localInertia = colShape.CalculateLocalInertia(mass);

            const float startX = StartPosX - ArraySizeX / 2;
            const float startY = StartPosY;
            const float startZ = StartPosZ - ArraySizeZ / 2;

            RigidBodyConstructionInfo rbInfo =
                new RigidBodyConstructionInfo(mass, null, colShape, localInertia);

            for (int k = 0; k < ArraySizeY; k++)
            {
                for (int i = 0; i < ArraySizeX; i++)
                {
                    for (int j = 0; j < ArraySizeZ; j++)
                    {
                        Matrix startTransform = Matrix.Translation(
                            2 * i + startX,
                            2 * k + startY,
                            2 * j + startZ
                        );

                        // using motionstate is recommended, it provides interpolation capabilities
                        // and only synchronizes 'active' objects
                        rbInfo.MotionState = new DefaultMotionState(startTransform);
                        RigidBody body = new RigidBody(rbInfo);

                        if (Body == null)
                        {
                            Body = body;
                        }

                        // make it drop from a height
                        body.Translate(new Vector3(0, 20, 0));

                        World.AddRigidBody(body);
                    }
                }
            }

            rbInfo.Dispose();*/
        }

        public override void Update()
        {
            base.Update();
            fall.GetWorldTransform(out Matrix transform);
            Debug.Log(transform);
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            using (Demo demo = new BasicDemo())
            {
                GraphicsLibraryManager.Run(demo);
            }
        }
    }
}
