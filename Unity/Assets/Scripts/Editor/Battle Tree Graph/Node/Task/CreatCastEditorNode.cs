using ET.Server;
using GraphProcessor;
using NPBehave;

namespace ET
{
    [NodeMenuItem("Tree/Task/Test", typeof(TreeGraph))]
    public class CreatCastEditorNode : TaskEditorNode
    {
        public int castConfigId;
        
        public override Task Init(Unit unit, Blackboard blackboard)
        {
            NP_Node = new CreatCastNode(castConfigId, unit);
            return NP_Node as Task;
        }
    }
}