using System;

namespace ET.Server
{

    public class LSFHandlerAttribute : BaseAttribute
    {
        public readonly Type CmdType;
        public readonly Type ComponentType;

        public LSFHandlerAttribute(Type cmdType, Type componentType)
        {
            this.CmdType = cmdType;
            this.ComponentType = componentType;
        }
    }
}