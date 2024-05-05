using ET.Node;
using NPBehave;

namespace ET
{
    public class SequenceNodeData: CompositeNodeData
    {
        public override Composite Init(Unit unit, ET.Node.Node[] nodes)
        {
            Sequence sequence = new Sequence(nodes);
            this.NP_Node = sequence;
            return sequence;
        }
    }
}