// 由Creat LSFCmd Editor生成

using Unity.Mathematics;

namespace ET.Server
{
    [LSFCmdHandler(typeof(LSFMoveCmd))]
    [FriendOfAttribute(typeof(ET.MoveComponent))]
    public class LSFMoveCmdHandler : LSFCmdHandler
    {
        public override void Receive(Unit unit, LSFCmd cmd)
        {
            var moveCmd = cmd as LSFMoveCmd;
            MoveComponent moveComponent = unit.GetComponent<MoveComponent>();
            float3 deltaDir = math.normalize(moveCmd.Position - unit.Position);
            unit.Position = moveComponent.Speed * deltaDir + unit.Position;
            unit.Rotation = moveCmd.Rotation;
        }
    }
}
