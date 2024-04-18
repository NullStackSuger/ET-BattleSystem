using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Decorator/Blackboard Condition", typeof(TreeGraph))]
    public class BlackboardConditionEditorNode: DecoratorEditorNode
    {
        [LabelText("运算符号")]
        public Operator Op = Operator.IS_EQUAL;

        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        public string Key;

        public override Decorator Init(Blackboard blackboard, Node node)
        {
            NP_Node = new BlackboardCondition(Key, Op, Stop, node);
            return this.NP_Node as Decorator;
        }
    }
}