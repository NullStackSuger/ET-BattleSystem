using System.Collections.Generic;
using ET.Node;
using NPBehave;

namespace ET
{
    [BsonDeserializerRegister]
    public class ParallelNodeData: CompositeNodeData
    {
        public readonly Policy SuccessPolicy = Policy.ALL;

        public readonly Policy FailurePolicy = Policy.ALL;

        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            List<ET.Node.Node> temp = new();
            foreach (NodeData child in this.Children)
            {
                temp.Add(child.NP_Node);
            }
            
            Parallel parallel = new Parallel(SuccessPolicy, FailurePolicy, temp.ToArray());
            this.NP_Node = parallel;
            return parallel;
        }
    }
}