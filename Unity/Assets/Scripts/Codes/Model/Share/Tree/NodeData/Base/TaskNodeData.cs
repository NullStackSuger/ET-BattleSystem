using NPBehave;

namespace ET
{
    public abstract class TaskNodeData : NodeData
    {
        public abstract Task Init(Unit unit, Blackboard blackboard);
    }
}