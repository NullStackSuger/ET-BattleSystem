using System;
using System.Collections.Generic;
using BulletSharp;
using BulletUnity;
using DemoFramework;
using UnityEngine;
using Matrix4x4 = System.Numerics.Matrix4x4;
using Quaternion = System.Numerics.Quaternion;
using Vector3 = System.Numerics.Vector3;

namespace ET.Client
{

    public class ShowBodyHelper : MonoBehaviour
    {
        [StaticField]
        public static ShowBodyHelper Instance;
        
        public Material mat;
        public Material groundMat;
        public GameObject ropePrefab;
        public GameObject softBodyPrefab;
        
        
        private readonly List<GameObject> createdObjs = new List<GameObject>();

        private void Awake()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            DestroyInGameView();
        }

        /// <summary>
        /// 在Unity中显示
        /// </summary>
        public void ShowInGameView(CollisionObject[] bodies)
        {
            foreach (CollisionObject co in bodies)
            {
                CollisionShape cs = co.CollisionShape;
                GameObject go;

                switch (cs.ShapeType)
                {
                    case BroadphaseNativeType.SoftBodyShape:
                    {
                        BulletSharp.SoftBody.SoftBody sb = (BulletSharp.SoftBody.SoftBody)co;
                        if (sb.Faces.Count == 0) go = CreateUnitySoftBodyRope(sb);
                        else go = CreateUnitySoftBodyCloth(sb);
                    } break;
                    case BroadphaseNativeType.CompoundShape:
                    {
                        //BulletSharp.Math.Matrix transform = co.WorldTransform;
                        go = new GameObject("Compund Shape");
                        BulletRigidBodyProxy rbp = go.AddComponent<BulletRigidBodyProxy>();
                        rbp.target = co as RigidBody;
                        foreach (BulletSharp.CompoundShapeChild child in (cs as CompoundShape).ChildList)
                        {
                            BulletSharp.Math.Matrix childTransform = child.Transform;
                            GameObject ggo = new GameObject(child.ToString());
                            MeshFilter mf = ggo.AddComponent<MeshFilter>();
                            Mesh m = mf.mesh;
                            MeshFactory2.CreateShape(child.ChildShape, m);
                            MeshRenderer mr = ggo.AddComponent<MeshRenderer>();
                            mr.sharedMaterial = mat;
                            ggo.transform.SetParent(go.transform);
                            UnityEngine.Matrix4x4 mt = childTransform.ToUnity();
                            ggo.transform.localPosition = BSExtensionMethods2.ExtractTranslationFromMatrix(ref mt);
                            ggo.transform.localRotation = BSExtensionMethods2.ExtractRotationFromMatrix(ref mt);
                            ggo.transform.localScale = BSExtensionMethods2.ExtractScaleFromMatrix(ref mt);

                            /*
                            BulletRigidBodyProxy rbp = ggo.AddComponent<BulletRigidBodyProxy>();
                            rbp.target = body;
                            return go;
                            */
                            //InitRigidBodyInstance(colObj, child.ChildShape, ref childTransform);
                        }
                    } break;
                    case BroadphaseNativeType.CapsuleShape:
                    {
                        CapsuleShape css = (CapsuleShape)cs;
                        GameObject ggo = GameObject.CreatePrimitive(PrimitiveType.Capsule);
                        GameObject.Destroy(ggo.GetComponent<Collider>());
                        go = new GameObject();
                        ggo.transform.parent = go.transform;
                        ggo.transform.localPosition = UnityEngine.Vector3.zero;
                        ggo.transform.localRotation = UnityEngine.Quaternion.identity;
                        ggo.transform.localScale = new UnityEngine.Vector3(css.Radius * 2f, css.HalfHeight * 2f, css.Radius * 2f);
                        BulletRigidBodyProxy rbp = go.AddComponent<BulletRigidBodyProxy>();
                        rbp.target = co;
                    } break;
                    default:
                    {
                        //Debug.Log("Creating " + cs.ShapeType + " for " + co.ToString());
                        go = CreateUnityCollisionObjectProxy(co as CollisionObject);
                    } break;
                }
                
                createdObjs.Add(go);
                //Debug.Log("Created Unity Shape for shapeType=" + co.CollisionShape.ShapeType + " collisionShape=" + co.ToString());
            }
        }
        /// <summary>
        /// 销毁当前显示
        /// </summary>
        public void DestroyInGameView()
        {
            for (int i = 0; i < createdObjs.Count; i++) 
            {
                Destroy(createdObjs[i]);
            }
            createdObjs.Clear();
        }
        
        private GameObject CreateUnitySoftBodyRope(BulletSharp.SoftBody.SoftBody body) {
            //determine what kind of soft body it is
            //rope
            GameObject rope = GameObject.Instantiate<GameObject>(ropePrefab);
            LineRenderer lr = rope.GetComponent<LineRenderer>();
            lr.SetVertexCount(body.Nodes.Count);
            BulletRopeProxy ropeProxy = rope.GetComponent<BulletRopeProxy>();
            ropeProxy.target = body;
            return rope;
        }
        
        private GameObject CreateUnitySoftBodyCloth(BulletSharp.SoftBody.SoftBody body) {
            //build nodes 2 verts map
            Dictionary<BulletSharp.SoftBody.Node, int> node2vertIdx = new Dictionary<BulletSharp.SoftBody.Node, int>();
            for (int i = 0; i < body.Nodes.Count; i++) {
                node2vertIdx.Add(body.Nodes[i], i);
            }
            List<int> tris = new List<int>();
            for (int i = 0; i < body.Faces.Count; i++) {
                BulletSharp.SoftBody.Face f = body.Faces[i];
                if (f.Nodes.Count != 3) {
                    Debug.LogError("Face was not a triangle");
                    continue;
                }
                for (int j = 0; j < f.Nodes.Count; j++) { 
                    tris.Add( node2vertIdx[f.Nodes[j]]);
                }
            }
            GameObject go = GameObject.Instantiate<GameObject>(softBodyPrefab);
            BulletSoftBodyProxy sbp = go.GetComponent<BulletSoftBodyProxy>();
            List<int> trisRev = new List<int>();
            for (int i = 0; i < tris.Count; i+=3) {
                trisRev.Add(tris[i]);
                trisRev.Add(tris[i + 2]);
                trisRev.Add(tris[i + 1]);
            }
            tris.AddRange(trisRev);
            sbp.target = body;
            sbp.verts = new UnityEngine.Vector3[body.Nodes.Count];
            sbp.tris = tris.ToArray();
            return go;
        }
        
        private GameObject CreateUnityCollisionObjectProxy(CollisionObject body) {
            if (body is GhostObject)
            {
                Debug.Log("ghost obj");
            }
            GameObject go = new GameObject(body.ToString());
            MeshFilter mf = go.AddComponent<MeshFilter>();
            Mesh m = mf.mesh;
            MeshFactory2.CreateShape(body.CollisionShape, m);
            MeshRenderer mr = go.AddComponent<MeshRenderer>();
            mr.sharedMaterial = mat;
            if (body.UserObject != null && body.UserObject.Equals("Ground")) {
                mr.sharedMaterial = groundMat;
            }
            BulletRigidBodyProxy rbp = go.AddComponent<BulletRigidBodyProxy>();
            rbp.target = body;
            return go;
        }
    }
}