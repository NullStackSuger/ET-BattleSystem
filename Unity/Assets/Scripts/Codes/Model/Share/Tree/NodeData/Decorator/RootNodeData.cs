using ET.Node;
using NPBehave;

namespace ET
{
    public class RootNodeData : DecoratorNodeData
    {
        public override Decorator Init(Unit unit, Blackboard blackboard, ET.Node.Node node)
        {
            ET.Node.Root root = new ET.Node.Root(node);
            this.NP_Node = root;
            return root;
        }
    }
}