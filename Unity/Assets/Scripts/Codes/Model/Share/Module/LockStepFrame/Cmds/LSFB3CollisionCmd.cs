// 由Creat LSFCmd Editor生成

using BulletSharp;
using ProtoBuf;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFB3CollisionCmd: LSFCmd
    {
        [ProtoMember(1)]
        public RigidBodyConstructionInfo BodyInfo;
        [ProtoMember(2)]
        public long OwnerId;
    }
}
