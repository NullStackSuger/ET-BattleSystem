// 由Creat LSFCmd Editor生成
using ProtoBuf;
using System.Collections.Generic;
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

        protected bool Equals(LSFTreeCmd other)
        {
            return base.Equals(other) && TreeId.Equals(other.TreeId) && BlackBoard.Equals(other.BlackBoard);
        }
    }
}
