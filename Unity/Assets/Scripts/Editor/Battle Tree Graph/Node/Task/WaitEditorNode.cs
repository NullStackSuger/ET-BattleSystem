using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Task/Wait", typeof(TreeGraph))]
    public class WaitEditorNode : TaskEditorNode
    {
        public float Seconds;
        
        public override Task Init(Blackboard blackboard)
        {
            NP_Node = new Wait(Seconds);
            return this.NP_Node as Wait;
        }
    }
}