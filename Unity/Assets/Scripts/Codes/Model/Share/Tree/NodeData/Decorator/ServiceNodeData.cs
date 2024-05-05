using ET.Node;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{    
    [BsonDeserializerRegister]
    public class ServiceNodeData : DecoratorNodeData
    {
        [LabelText("委托执行时间间隔")]
        public float Interval;
        
        public System.Action Action;
        
        public override Node.Node Init(Unit unit, Blackboard blackboard)
        {
            Service service = new Service(Interval, Action, this.Child.NP_Node);
            this.NP_Node = service;
            return service;
        }
    }
}