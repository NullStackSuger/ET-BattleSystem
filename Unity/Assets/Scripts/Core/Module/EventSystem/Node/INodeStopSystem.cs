using System;

namespace ET
{
    public interface INodeStop
    {
    }
    
    public interface INodeStopSystem : ISystemType
    {
        void Run(Entity o);
    }
    
    [ObjectSystem]
    public abstract class NodeStopSystem<T> : INodeStopSystem where T: Entity, INodeStop
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeStopSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStopSystem.Run(Entity o)
        {
            this.NodeStart((T)o);
        }

        protected abstract void NodeStart(T self);
    }
}