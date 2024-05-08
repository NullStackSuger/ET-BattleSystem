using System;

namespace ET
{
    public class NodeRunAttribute: BaseAttribute
    {
        public Type Type { get; }

        public NodeRunAttribute(Type type)
        {
            Type = type;
        }
    }
    
    public interface INodeRun
    {
        
    }
    
    public abstract class NodeRun
    {
        public abstract ETTask<bool> Run(Entity self, TreeComponent tree);
    }
}