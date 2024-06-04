// 由Creat LSFCmd Editor生成
using ProtoBuf;
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
    }
}
