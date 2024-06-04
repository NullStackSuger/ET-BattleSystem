// 由Creat LSFCmd Editor生成

using Unity.Mathematics;

namespace ET.Server
{
    [LSFHandler(typeof(MoveComponent), typeof(LSFMoveCmd))]
    [FriendOf(typeof(GameRoomComponent))]
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
            
        }

        public override void Receive(Unit unit, LSFMoveCmd cmd)
        {

        }
    }
}
