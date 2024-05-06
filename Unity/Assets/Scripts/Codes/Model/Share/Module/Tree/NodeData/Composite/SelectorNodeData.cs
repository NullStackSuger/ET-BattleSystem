using System.Collections.Generic;
using ET.Node;
using NPBehave;

namespace ET
{
    [BsonDeserializerRegister]
    public class SelectorNodeData: CompositeNodeData
    {
        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            List<ET.Node.Node> temp = new();
            foreach (NodeData child in this.Children)
            {
                temp.Add(child.NP_Node);
            }
            
            Selector selector = new Selector(temp.ToArray());
            this.NP_Node = selector;
            return selector;
        }
    }
}