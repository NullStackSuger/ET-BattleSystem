using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{    
    public class ServiceNodeData : DecoratorNodeData
    {
        [LabelText("委托执行时间间隔")]
        public float Interval;
        
        public System.Action Action;
        
        public override Decorator Init(Unit unit,  Blackboard blackboard, Node node)
        {
            Service service = new Service(Interval, Action, node);
            this.NP_Node = service;
            return service;
        }
    }
}