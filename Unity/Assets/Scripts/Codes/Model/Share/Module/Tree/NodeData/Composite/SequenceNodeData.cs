using System.Collections.Generic;
using ET.Node;
using NPBehave;

namespace ET
{
    [BsonDeserializerRegister]
    public class SequenceNodeData: CompositeNodeData
    {
        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            List<ET.Node.Node> temp = new();
            foreach (NodeData child in this.Children)
            {
                temp.Add(child.NP_Node);
            }
            
            Sequence sequence = new Sequence(temp.ToArray());
            this.NP_Node = sequence;
            return sequence;
        }
    }
}