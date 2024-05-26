namespace ET.Server
{
    /// <summary>
    /// 接收处理对应消息
    /// </summary>
    [LSFCmdHandler]
    public abstract class ILSFCmdHandler<T> where T : LSFCmd
    {
        public abstract void Receive(T cmd);
    }

    public class LSFCmdHandlerAttribute: BaseAttribute
    {
    }
}