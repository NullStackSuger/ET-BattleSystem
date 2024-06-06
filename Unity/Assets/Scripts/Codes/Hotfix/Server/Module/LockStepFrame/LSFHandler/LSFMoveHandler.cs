// 由Creat LSFCmd Editor生成

using Unity.Mathematics;

namespace ET.Server
{
    [LSFHandler(typeof(MoveComponent), typeof(LSFMoveCmd))]
    [FriendOf(typeof(GameRoomComponent))]
    [FriendOf(typeof(MoveComponent))]
    public class LSFMoveHandler : LSFHandler<MoveComponent, LSFMoveCmd>
    {
        public override void TickStart(GameRoomComponent room, MoveComponent component)
        {

        }

        public override void Tick(GameRoomComponent room, MoveComponent component)
        {

        }

        public override void TickEnd(GameRoomComponent room, MoveComponent component)
        {
            Unit unit = component.GetParent<Unit>();
            LSFMoveCmd cmd = new()
            {
                Frame = room.Frame,
                UnitId = unit.Id,
                Position = unit.Position,
                Rotation = unit.Rotation,
            };
            room.AddToSend(cmd);
        }

        public override void Receive(Unit unit, MoveComponent component, LSFMoveCmd cmd)
        {
            float3 moveDir = math.normalizesafe(cmd.Position - unit.Position);
            unit.Position += moveDir * component.Speed;
            unit.Rotation = cmd.Rotation;
        }
    }
}
