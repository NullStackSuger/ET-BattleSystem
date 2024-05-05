using ET.Node;

namespace ET
{
    public abstract class CompositeNodeData : NodeData
    {
        public NodeData[] Children;
        public abstract Composite Init(Unit unit, ET.Node.Node[] nodes);
    }
}