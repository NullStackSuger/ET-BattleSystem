using GraphProcessor;
using NPBehave;
using UnityEngine;

namespace ET
{
    [NodeMenuItem("Tree/Decorator/Root", typeof(TreeGraph))]
    public class RootEditorNode : DecoratorEditorNode
    {
        public override Decorator Init(Blackboard blackboard, Node node)
        {
            NP_Node = new NPBehave.Root(node);
            return this.NP_Node as Decorator;
        }
    }
}