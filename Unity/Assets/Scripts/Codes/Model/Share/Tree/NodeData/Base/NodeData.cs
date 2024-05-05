using NPBehave;

namespace ET
{
    public abstract class NodeData
    {
        public Node.Node NP_Node;

        public abstract Node.Node Init(Unit unit, Blackboard blackboard);
    }
}