using System;

namespace ET
{
    /// <summary>
    /// 接收处理对应消息
    /// </summary>
    public abstract class LSFCmdHandler
    {
        public abstract void Receive(LSFCmd cmd);
    }

    public class LSFCmdHandlerAttribute: BaseAttribute
    {
        public Type Type;
        public LSFCmdHandlerAttribute(Type type)
        {
            this.Type = type;
        }
    }
}