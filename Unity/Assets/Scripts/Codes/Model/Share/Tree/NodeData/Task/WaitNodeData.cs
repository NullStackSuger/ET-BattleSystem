using NPBehave;

namespace ET
{
    public class WaitNodeData : TaskNodeData
    {
        public float Seconds;
        
        public override Task Init(Unit unit, Blackboard blackboard)
        {
            Wait wait = new Wait(Seconds);
            this.NP_Node = wait;
            return wait;
        }
    }
}