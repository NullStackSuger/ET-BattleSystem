// 由Creat LSFCmd Editor生成
using ProtoBuf;
using System.Collections.Generic;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFB3CollisionCmd: LSFCmd
    {
        [ProtoMember(1)]
        public BulletSharp.RigidBodyConstructionInfo BodyInfo;
        [ProtoMember(2)]
        public System.Int64 OwnerId;

        protected bool Equals(LSFB3CollisionCmd other)
        {
            return base.Equals(other) && this.BodyInfo.Equals(other.BodyInfo) && this.OwnerId.Equals(other.OwnerId);
        }
    }
}
