using NPBehave;

namespace ET
{
    public abstract class CompositeNodeData : NodeData
    {
        public NodeData[] Children;
        public abstract Composite Init(Unit unit, Node[] nodes);
    }
}