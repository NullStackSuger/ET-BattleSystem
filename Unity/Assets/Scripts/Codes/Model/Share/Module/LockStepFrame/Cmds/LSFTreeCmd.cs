// 由Creat LSFCmd Editor生成
using ProtoBuf;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFTreeCmd: LSFCmd
    {
        public string TreeName;

        public BlackBoard BlackBoard;
    }
}
