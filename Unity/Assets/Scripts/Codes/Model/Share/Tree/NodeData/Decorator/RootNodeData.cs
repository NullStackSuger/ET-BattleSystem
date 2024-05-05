using ET.Node;
using NPBehave;

namespace ET
{
    [BsonDeserializerRegister]
    public class RootNodeData : DecoratorNodeData
    {
        public Blackboard Blackboard;
        
        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            ET.Node.Root root = new ET.Node.Root(Blackboard, this.Child.NP_Node);
            this.NP_Node = root;
            return root;
        }
    }
}