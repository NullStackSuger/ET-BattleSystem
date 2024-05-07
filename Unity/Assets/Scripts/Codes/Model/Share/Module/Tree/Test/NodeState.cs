namespace ET
{

    public enum NodeState : byte
    {
        UnRun = 0,
        Running = 1,
        True = 1 << 1,
        False = 1 << 2,
    }
}