using System;

namespace ET.Client
{

    public class LSFHandlerAttribute : BaseAttribute
    {
        public readonly Type CmdType;
        public readonly Type ComponentType;

        public LSFHandlerAttribute(Type ComponentType, Type CmdType)
        {
            this.CmdType = CmdType;
            this.ComponentType = ComponentType;
        }
    }
}