using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Decorator/BlackboardCondition", typeof(TreeGraph))]
    public class BlackboardConditionEditorNode: DecoratorEditorNode
    {
        [LabelText("运算符号")]
        public Operator Op = Operator.IS_EQUAL;

        [LabelText("终止条件")]
        public Stops Stop = Stops.IMMEDIATE_RESTART;

        public string Key;

        public override object Init(Blackboard blackboard, object node)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.BlackboardConditionNodeData");
            NodeHelper.SetField(this.NodeData, ("Key", this.Key), ("Op", this.Op), ("Stop", this.Stop));
            return this.NodeData;
        }
    }
}