// 由Creat LSFCmd Editor生成

using BulletSharp;
using ProtoBuf;
namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFB3CollisionCmd: LSFCmd
    {
        public RigidBodyConstructionInfo BodyInfo;
        public long OwnerId;
    }
}
