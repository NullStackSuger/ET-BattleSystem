using ProtoBuf;

namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFTestCmd: LSFCmd
    {
        public string Value = "Test";
    }
}