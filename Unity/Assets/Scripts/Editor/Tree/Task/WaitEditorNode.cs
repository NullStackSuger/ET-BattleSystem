using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Task/Wait", typeof(TreeGraph))]
    public class WaitEditorNode : TaskEditorNode
    {
        public float Seconds;
        
        public override object Init(Blackboard blackboard)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.WaitNodeData");
            NodeHelper.SetField(this.NodeData,  ("Seconds", this.Seconds));
            return this.NodeData;
        }
    }
}