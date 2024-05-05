using NPBehave;

namespace ET
{
    public class RootNodeData : DecoratorNodeData
    {
        public override Decorator Init(Unit unit, Blackboard blackboard, Node node)
        {
            NPBehave.Root root = new NPBehave.Root(node);
            this.NP_Node = root;
            return root;
        }
    }
}