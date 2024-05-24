namespace ET
{
    public abstract class LSFCmd
    {
        public uint Frame;
        public long UnitId;

        public abstract bool Check(LSFCmd cmd);
    }
}