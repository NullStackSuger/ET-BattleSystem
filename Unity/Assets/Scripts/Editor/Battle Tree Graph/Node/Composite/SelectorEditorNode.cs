using GraphProcessor;
using NPBehave;

namespace ET
{
    [NodeMenuItem("Tree/Composite/Selector", typeof(TreeGraph))]
    public class SelectorEditorNode: CompositeEditorNode
    {
        public override Composite Init(Node[] nodes)
        {
            this.NP_Node = new Selector(nodes);
            return this.NP_Node as Selector;
        }
    }
}