using GraphProcessor;
using NPBehave;

namespace ET
{
    [NodeMenuItem("Tree/Composite/Sequence", typeof(TreeGraph))]
    public class SequenceEditorNode: CompositeEditorNode
    {
        public override Composite Init(Node[] nodes)
        {
            this.NP_Node = new Sequence(nodes);
            return this.NP_Node as Sequence;
        }
    }
}