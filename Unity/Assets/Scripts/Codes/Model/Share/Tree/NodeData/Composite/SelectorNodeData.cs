using ET.Node;
using NPBehave;

namespace ET
{
    public class SelectorNodeData: CompositeNodeData
    {
        public override Composite Init(Unit unit, ET.Node.Node[] nodes)
        {
            Selector selector = new Selector(nodes);
            this.NP_Node = selector;
            return selector;
        }
    }
}