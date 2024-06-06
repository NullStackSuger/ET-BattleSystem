// 由Creat LSFCmd Editor生成
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFCastCmd: LSFCmd
    {
        [ProtoMember(1)]
        public List<Cast> Casts;

        protected bool Equals(LSFCastCmd other)
        {
            return base.Equals(other) && this.Casts.Equals(other.Casts);
        }
    }
}
