// 由Creat LSFCmd Editor生成

using ProtoBuf;
using Unity.Mathematics;

namespace ET
{
    [ProtoContract]
    [ProtobufBaseTypeRegister]
    public class LSFMoveCmd: LSFCmd
    {
        [ProtoMember(1)]
        public float3 Position;
        [ProtoMember(2)]
        public quaternion Rotation;
    }
}
