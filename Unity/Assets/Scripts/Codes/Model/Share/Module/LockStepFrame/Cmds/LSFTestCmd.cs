// 由Creat LSFCmd Editor生成
using ProtoBuf;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFTestCmd: LSFCmd
    {
        [ProtoMember(1)]
        public System.String Value;
    }
}
