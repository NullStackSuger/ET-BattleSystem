using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [BoxGroup("等待到停止节点数据")]
    [HideLabel]
    [NodeMenuItem("Tree/Task/Wait Until Stopped", typeof(TreeGraph))]
    public class WaitUntilStopped: TaskEditorNode
    {
        public override Task Init(Blackboard blackboard)
        {
            this.NP_Node = new NPBehave.WaitUntilStopped();
            return this.NP_Node as Task;
        }
    }
}