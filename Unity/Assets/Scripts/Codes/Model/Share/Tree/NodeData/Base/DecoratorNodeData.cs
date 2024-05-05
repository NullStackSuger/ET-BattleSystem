using ET.Node;
using NPBehave;

namespace ET
{
    public abstract class DecoratorNodeData : NodeData
    {
        public NodeData Child;
        
        public abstract Decorator Init(Unit unit, Blackboard blackboard, ET.Node.Node node);
    }
}