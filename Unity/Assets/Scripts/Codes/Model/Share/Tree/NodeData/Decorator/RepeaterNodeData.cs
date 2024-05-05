using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    public class RepeaterNodeData : DecoratorNodeData
    {
        private int loopCount = -1;
        
        public override Decorator Init(Unit unit, Blackboard blackboard, Node node)
        {
            Repeater repeater = new Repeater(loopCount, node);
            this.NP_Node = repeater;
            return repeater;
        }
    }
}