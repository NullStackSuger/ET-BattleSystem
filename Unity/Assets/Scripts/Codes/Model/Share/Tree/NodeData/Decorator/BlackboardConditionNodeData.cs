using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    public class BlackboardConditionNodeData: DecoratorNodeData
    {
        [LabelText("运算符号")]
        public Operator Op = Operator.IS_EQUAL;

        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        public string Key;

        public override Decorator Init(Unit unit, Blackboard blackboard, Node node)
        {
            BlackboardCondition blackboardCondition = new BlackboardCondition(Key, Op, Stop, node);
            this.NP_Node = blackboardCondition;
            return blackboardCondition;
        }
    }
}