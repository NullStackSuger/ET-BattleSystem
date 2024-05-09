namespace ET
{
    public class LogNode : Entity, INode, IAwake<string>, IDestroy
    {
        public string Message;
    }
}