using System.Linq;
using System.Reflection;
using GraphProcessor;
using NPBehave;
using UnityEngine;
using Task = NPBehave.Task;

namespace ET
{
    [NodeMenuItem("Tree/Task/Test", typeof(TreeGraph))]
    public class TestEditorNode : TaskEditorNode
    {
        public override Task Init(Blackboard blackboard)
        {
            //NP_Node = new TestNode();
            
            // Editor环境下，Model.dll.bytes已经被自动加载，不需要加载，重复加载反而会出问题
            // Editor下无需加载，直接查找获得HotUpdate程序集
            
            // 问题: 这个程序集好像不对, 没TestNode里面
            Assembly model = System.AppDomain.CurrentDomain.GetAssemblies().First(a => a.GetName().Name == "Unity.Model");
            Debug.Log("Get Assembly: " + model != null);

            foreach (var type in model.GetTypes())
            {
                //if (type.ToString().EndsWith("Node"))
                    Debug.Log("Get Class: " + type.ToString());
            }
            
            Debug.Log("Get TestNode: " + model.CreateInstance("ET.Client.TestNode") != null);
            this.NP_Node = model.CreateInstance("ET.Client.TestNode") as Node;
            Debug.Log("Get TestNode as Node: " + NP_Node != null);
            return NP_Node as Task;
        }
    }
}