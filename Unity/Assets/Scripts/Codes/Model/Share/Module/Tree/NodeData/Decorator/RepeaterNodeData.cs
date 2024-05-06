using ET.Node;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [BsonDeserializerRegister]
    public class RepeaterNodeData : DecoratorNodeData
    {
        private int loopCount = -1;
        
        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            Repeater repeater = new Repeater(loopCount, this.Child.NP_Node);
            this.NP_Node = repeater;
            return repeater;
        }
    }
}