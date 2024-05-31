using ProtoBuf;

namespace ET
{
    [ProtoContract]
    public class LSFTestCmd: LSFCmd
    {
        public string Value = "Test";
    }
}