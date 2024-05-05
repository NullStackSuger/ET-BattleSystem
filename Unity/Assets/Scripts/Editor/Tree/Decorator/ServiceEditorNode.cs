using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{    
    [NodeMenuItem("Tree/Decorator/Service", typeof(TreeGraph))]
    public class ServiceEditorNode : DecoratorEditorNode
    {
        [LabelText("委托执行时间间隔")]
        public float Interval;
        
        public System.Action Action;
        
        public override object Init(Blackboard blackboard, object node)
        {
            this.NodeData = NodeHelper.CreatNodeData("ServiceNodeData",
                ("Interval", this.Interval), ("Action", this.Action));
            return this.NodeData;
        }
    }
}