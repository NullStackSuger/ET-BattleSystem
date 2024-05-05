using NPBehave;

namespace ET
{
    public class ParallelNodeData: CompositeNodeData
    {
        public readonly Policy SuccessPolicy = Policy.ALL;

        public readonly Policy FailurePolicy = Policy.ALL;

        public override Composite Init(Unit unit, Node[] nodes)
        {
            Parallel parallel = new Parallel(SuccessPolicy, FailurePolicy, nodes);
            this.NP_Node = parallel;
            return parallel;
        }
    }
}