namespace ET
{
    public interface INodeRun{}
    public interface INodeRun<A>{}
    public interface INodeRun<A, B>{}
    public interface INodeRun<A, B, C>{}
    public interface INodeRun<A, B, C, D>{}
    [ObjectSystem]
    public abstract class NodeRunSystem<T> where T : Entity, INodeRun
    {
        public abstract ETTask<bool> Run(T self);
    }
    [ObjectSystem]
    public abstract class NodeRunSystem<T, A> where T : Entity, INodeRun<A>
    {
        public abstract ETTask<bool> Run(T self, A a);
    }
    [ObjectSystem]
    public abstract class NodeRunSystem<T, A, B> where T : Entity, INodeRun<A, B>
    {
        public abstract ETTask<bool> Run(T self, A a, B b);
    }
    [ObjectSystem]
    public abstract class NodeRunSystem<T, A, B, C> where T : Entity, INodeRun<A, B, C>
    {
        public abstract ETTask<bool> Run(T self, A a, B b, C c);
    }
    [ObjectSystem]
    public abstract class NodeRunSystem<T, A, B, C, D> where T : Entity, INodeRun<A, B, C, D>
    {
        public abstract ETTask<bool> Run(T self, A a, B b, C c, D d);
    }
}