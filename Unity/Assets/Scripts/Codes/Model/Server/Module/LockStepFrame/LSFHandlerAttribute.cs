using System;

namespace ET.Server
{

    public class LSFHandlerAttribute : BaseAttribute
    {
        public readonly Type CmdType;
        public readonly Type ComponentType;

        public LSFHandlerAttribute(Type componentType, Type cmdType)
        {
            this.CmdType = cmdType;
            this.ComponentType = componentType;
        }
    }
}