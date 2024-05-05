using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Composite/Parallel", typeof(TreeGraph))]
    public class ParallelEditorNode: CompositeEditorNode
    {
        [LabelText("成功政策")]
        public readonly Policy SuccessPolicy = Policy.ALL;

        [LabelText("失败政策")]
        public readonly Policy FailurePolicy = Policy.ALL;

        public override object Init(object[] nodes)
        {
            this.NodeData = NodeHelper.CreatNodeData("ParallelNodeData", 
                ("SuccessPolicy", this.SuccessPolicy), ("FailurePolicy", this.FailurePolicy));
            return this.NodeData;
        }
    }
}