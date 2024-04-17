using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Decorator/Repeater", typeof(TreeGraph))]
    public class RepeaterEditorNode : DecoratorEditorNode
    {
        private int loopCount = -1;
        
        public override Decorator Init(Unit unit, Blackboard blackboard, Node node)
        {
            this.NP_Node = new Repeater(loopCount, node);
            return this.NP_Node as Decorator;
        }
    }
}