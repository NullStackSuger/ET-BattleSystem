// 由Creat LSFCmd Editor生成

using System.Collections.Generic;
using ProtoBuf;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFCastCmd: LSFCmd
    {
        [ProtoMember(1)]
        public List<Cast> Casts = new();
    }
}
