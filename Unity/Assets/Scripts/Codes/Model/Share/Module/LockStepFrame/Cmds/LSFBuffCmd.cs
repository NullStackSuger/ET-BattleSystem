// 由Creat LSFCmd Editor生成

using System.Collections.Generic;
using ProtoBuf;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFBuffCmd: LSFCmd
    {
        [ProtoMember(1)]
        public Dictionary<int, Buff> Buffs = new();
    }
}
