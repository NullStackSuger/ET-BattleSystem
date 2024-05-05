using ET.Node;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [BsonDeserializerRegister]
    public class BlackboardConditionNodeData: DecoratorNodeData
    {
        [LabelText("运算符号")]
        public Operator Op = Operator.IS_EQUAL;

        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        public string Key;

        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            BlackboardCondition blackboardCondition = new BlackboardCondition(Key, Op, Stop, this.Child.NP_Node);
            this.NP_Node = blackboardCondition;
            return blackboardCondition;
        }
    }
}