using NPBehave;

namespace ET
{
    public class SequenceNodeData: CompositeNodeData
    {
        public override Composite Init(Unit unit, Node[] nodes)
        {
            Sequence sequence = new Sequence(nodes);
            this.NP_Node = sequence;
            return sequence;
        }
    }
}