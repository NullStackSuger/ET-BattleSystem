// 由Creat LSFCmd Editor生成
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFBuffCmd: LSFCmd
    {
        [ProtoMember(1)]
        public Dictionary<int, Buff> Buffs;

        protected bool Equals(LSFBuffCmd other)
        {
            return base.Equals(other) && this.Buffs.Equals(other.Buffs);
        }
    }
}
