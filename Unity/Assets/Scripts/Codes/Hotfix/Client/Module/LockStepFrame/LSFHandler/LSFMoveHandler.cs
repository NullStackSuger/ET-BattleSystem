// 由Creat LSFCmd Editor生成

using Unity.Mathematics;

namespace ET.Client
{
    [LSFHandler(typeof(MoveComponent), typeof(LSFMoveCmd))]
    [FriendOf(typeof(GameRoomComponent))]
    public class LSFMoveHandler : LSFHandler<MoveComponent, LSFMoveCmd>
    {
        public override void TickStart(GameRoomComponent room, MoveComponent component, bool inRollBack)
        {

        }

        public override void Tick(GameRoomComponent room, MoveComponent component, bool inRollBack)
        {

        }

        public override LSFCmd TickEnd(GameRoomComponent room, MoveComponent component, bool inRollBack)
        {
            // 这里通常写Tick结束的操作类似LateUpdate
            
            Unit unit = component.GetParent<Unit>();
            
            LSFMoveCmd moveCmd = new()
            {
                Frame = room.Frame,
                UnitId = unit.Id,
                Position = unit.Position,
                Rotation = unit.Rotation,
            };

            if (!inRollBack)
            {
                room.AddToSend(moveCmd);
            }

            return moveCmd;
        }

        public override void Receive(Unit unit, MoveComponent component, LSFMoveCmd cmd)
        {
            unit.Position = cmd.Position;
            unit.Rotation = cmd.Rotation;
        }

        public override bool Check(LSFMoveCmd clientCmd, LSFMoveCmd serverCmd)
        {
            return math.distance(clientCmd.Position, serverCmd.Position) < 0.5 && clientCmd.Rotation.Equals(serverCmd.Rotation);
        }

        public override void RollBack(GameRoomComponent room, MoveComponent component, LSFMoveCmd cmd)
        {
            
        }
    }
}
