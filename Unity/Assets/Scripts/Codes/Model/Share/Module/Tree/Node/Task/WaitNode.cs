namespace ET
{
    public class WaitNode : Entity, INode, IAwake<long>, IDestroy
    {
        public long Seconds; // S
    }
}