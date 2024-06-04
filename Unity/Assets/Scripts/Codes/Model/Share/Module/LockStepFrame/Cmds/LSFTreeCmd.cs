// 由Creat LSFCmd Editor生成
using ProtoBuf;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFTreeCmd: LSFCmd
    {
        [ProtoMember(1)]
        public System.Int64 TreeId;
        [ProtoMember(2)]
        public ET.BlackBoard BlackBoard;
    }
}
