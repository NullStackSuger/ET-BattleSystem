using System;

namespace ET
{
    public interface INodeStart
    {
    }
    public interface INodeStart<A>
    {
    }
    public interface INodeStart<A, B>
    {
    }
    public interface INodeStart<A, B, C>
    {
    }
    public interface INodeStart<A, B, C, D>
    {
    }
    
    public interface INodeStartSystem : ISystemType
    {
        void Run(Entity o);
    }
    public interface INodeStartSystem<A> : ISystemType
    {
        void Run(Entity o, A a);
    }
    public interface INodeStartSystem<A, B> : ISystemType
    {
        void Run(Entity o, A a, B b);
    }
    public interface INodeStartSystem<A, B, C> : ISystemType
    {
        void Run(Entity o, A a, B b, C c);
    }
    public interface INodeStartSystem<A, B, C, D> : ISystemType
    {
        void Run(Entity o, A a, B b, C c, D d);
    }
    
    [ObjectSystem]
    public abstract class NodeStartSystem<T> : INodeStartSystem where T: Entity, INodeStart
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeStartSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem.Run(Entity o)
        {
            this.NodeStart((T)o);
        }

        protected abstract void NodeStart(T self);
    }
    [ObjectSystem]
    public abstract class NodeStartSystem<T, A> : INodeStartSystem<A> where T: Entity, INodeStart<A>
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeStartSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem<A>.Run(Entity o, A a)
        {
            this.NodeStart((T)o, a);
        }

        protected abstract void NodeStart(T self, A a);
    }
    [ObjectSystem]
    public abstract class NodeStartSystem<T, A, B> : INodeStartSystem<A, B> where T: Entity, INodeStart<A, B>
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeStartSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem<A, B>.Run(Entity o, A a, B b)
        {
            this.NodeStart((T)o, a, b);
        }

        protected abstract void NodeStart(T self, A a, B b);
    }
    [ObjectSystem]
    public abstract class NodeStartSystem<T, A, B, C> : INodeStartSystem<A, B, C> where T: Entity, INodeStart<A, B, C>
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeStartSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem<A ,B, C>.Run(Entity o, A a, B b, C c)
        {
            this.NodeStart((T)o, a, b, c);
        }

        protected abstract void NodeStart(T self, A a, B b, C c);
    }
    [ObjectSystem]
    public abstract class NodeStartSystem<T, A, B, C, D> : INodeStartSystem<A, B, C, D> where T: Entity, INodeStart<A, B, C, D>
    {
        Type ISystemType.Type()
        {
            return typeof(T);
        }

        Type ISystemType.SystemType()
        {
            return typeof(INodeStartSystem);
        }

        InstanceQueueIndex ISystemType.GetInstanceQueueIndex()
        {
            return InstanceQueueIndex.None;
        }

        void INodeStartSystem<A ,B, C, D>.Run(Entity o, A a, B b, C c, D d)
        {
            this.NodeStart((T)o, a, b, c, d);
        }

        protected abstract void NodeStart(T self, A a, B b, C c, D d);
    }
}