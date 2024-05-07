using System;

namespace ET
{
    // 有这个接口说明是节点
    public interface INode : INodeStart, INodeStop
    {
        
    }
    public interface INode<A> : INodeStart<A>, INodeStop
    {
        
    }
    public interface INode<A, B> : INodeStart<A, B>, INodeStop
    {
        
    }
    public interface INode<A, B, C> : INodeStart<A, B, C>, INodeStop
    {
        
    }
    public interface INode<A, B, C, D> : INodeStart<A, B, C, D>, INodeStop
    {
        
    }

    public interface INodeSystem: INodeStartSystem, INodeStopSystem
    {
        
    }
    public interface INodeSystem<A>: INodeStartSystem<A>, INodeStopSystem
    {
        
    }
    public interface INodeSystem<A, B>: INodeStartSystem<A, B>, INodeStopSystem
    {
        
    }
    public interface INodeSystem<A, B, C>: INodeStartSystem<A, B, C>, INodeStopSystem
    {
        
    }
    public interface INodeSystem<A, B, C, D>: INodeStartSystem<A, B, C, D>, INodeStopSystem
    {
        
    }
    
    [ObjectSystem]
    public abstract class NodeSystem<T> : INodeSystem where T: Entity, INode
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem.Run(Entity o)
        {
            this.NodeStart((T)o);
        }
        
        void INodeStopSystem.Run(Entity o)
        {
            this.NodeStop((T)o);
        }

        protected abstract void NodeStart(T self);
        protected abstract void NodeStop(T self);
    }
    [ObjectSystem]
    public abstract class NodeSystem<T, A> : INodeSystem<A> where T: Entity, INode<A>
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem<A>.Run(Entity o, A a)
        {
            this.NodeStart((T)o, a);
        }
        void INodeStopSystem.Run(Entity o)
        {
            this.NodeStop((T)o);
        }
        
        protected abstract void NodeStart(T self, A a);
        protected abstract void NodeStop(T self);
    }
    [ObjectSystem]
    public abstract class NodeSystem<T, A, B> : INodeSystem<A, B> where T: Entity, INode<A, B>
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem<A, B>.Run(Entity o, A a, B b)
        {
            this.NodeStart((T)o, a, b);
        }
        void INodeStopSystem.Run(Entity o)
        {
            this.NodeStop((T)o);
        }
        
        protected abstract void NodeStart(T self, A a, B b);
        protected abstract void NodeStop(T self);
    }
    [ObjectSystem]
    public abstract class NodeSystem<T, A, B, C> : INodeSystem<A, B, C> where T: Entity, INode<A, B, C>
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem<A, B, C>.Run(Entity o, A a, B b, C c)
        {
            this.NodeStart((T)o, a, b, c);
        }
        void INodeStopSystem.Run(Entity o)
        {
            this.NodeStop((T)o);
        }
        
        protected abstract void NodeStart(T self, A a, B b, C c);
        protected abstract void NodeStop(T self);
    }
    [ObjectSystem]
    public abstract class NodeSystem<T, A, B, C, D> : INodeSystem<A, B, C, D> where T: Entity, INode<A, B, C, D>
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem<A, B, C, D>.Run(Entity o, A a, B b, C c, D d)
        {
            this.NodeStart((T)o, a, b, c, d);
        }
        void INodeStopSystem.Run(Entity o)
        {
            this.NodeStop((T)o);
        }
        
        protected abstract void NodeStart(T self, A a, B b, C c, D d);
        protected abstract void NodeStop(T self);
    }
}