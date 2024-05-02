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
            this.NP_Node = TreeGraph.Model.CreateInstance("ET.Client.TestNode") as Node;
            return NP_Node as Task;
        }
    }
}