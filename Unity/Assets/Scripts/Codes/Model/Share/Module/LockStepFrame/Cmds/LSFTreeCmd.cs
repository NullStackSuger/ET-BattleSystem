// 由Creat LSFCmd Editor生成
using ProtoBuf;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFTreeCmd: LSFCmd
    {
        [ProtoMember(1)]
        public long TreeId;
        [ProtoMember(2)]
        public BlackBoard BlackBoard;
    }
}
