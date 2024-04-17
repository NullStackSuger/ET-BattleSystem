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
        
        public override Decorator Init(Unit unit, Blackboard blackboard, Node node)
        {
            NP_Node = new Service(Interval, Action, node);
            return this.NP_Node as Decorator;
        }
    }
}