using System;

namespace ET
{

    public class NodeHandlerAttribute: BaseAttribute
    {
        public Type NodeType { get; }

        public NodeHandlerAttribute(Type type)
        {
            this.NodeType = type;
        }
    }
}