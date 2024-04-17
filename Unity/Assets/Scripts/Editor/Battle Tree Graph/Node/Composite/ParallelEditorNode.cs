using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Composite/Parallel", typeof(TreeGraph))]
    public class ParallelEditorNode: CompositeEditorNode
    {
        [LabelText("成功政策")]
        public readonly Parallel.Policy SuccessPolicy = Parallel.Policy.ALL;

        [LabelText("失败政策")]
        public readonly Parallel.Policy FailurePolicy = Parallel.Policy.ALL;

        public override Composite Init(Node[] nodes)
        {
            this.NP_Node = new Parallel(SuccessPolicy, FailurePolicy, nodes);
            return this.NP_Node as Parallel;
        }
    }
}