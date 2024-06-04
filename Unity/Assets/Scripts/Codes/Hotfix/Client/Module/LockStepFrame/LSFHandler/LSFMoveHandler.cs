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

        public override void TickEnd(GameRoomComponent room, MoveComponent component, bool inRollBack)
        {
            if (!inRollBack)
            {
                Unit unit = component.GetParent<Unit>();
                LSFMoveCmd moveCmd = new()
                {
                    Frame = room.Frame,
                    Position = unit.Position,
                    Rotation = unit.Rotation,
                };
                room.AddToSend(moveCmd);
            }
            else
            {
                Log.Warning("RollBack TickEnd");
            }
        }

        public override void Receive(Unit unit, LSFMoveCmd cmd)
        {

        }

        public override bool Check(GameRoomComponent room, MoveComponent component, LSFMoveCmd cmd)
        {
            return true;
        }

        public override void RollBack(GameRoomComponent room, MoveComponent component, LSFMoveCmd cmd)
        {

        }
    }
}
