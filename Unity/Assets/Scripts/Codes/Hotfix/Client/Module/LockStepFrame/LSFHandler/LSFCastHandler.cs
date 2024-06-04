// 由Creat LSFCmd Editor生成
namespace ET.Client
{
    [LSFHandler(typeof(CastComponent), typeof(LSFCastCmd))]
    [FriendOf(typeof(GameRoomComponent))]
    public class LSFCastHandler: LSFHandler<CastComponent, LSFCastCmd>
    {
        public override void TickStart(GameRoomComponent room, CastComponent component, bool inRollBack)
        {

        }
        public override void Tick(GameRoomComponent room, CastComponent component, bool inRollBack)
        {

        }
        public override void TickEnd(GameRoomComponent room, CastComponent component, bool inRollBack)
        {

        }

        public override void Receive(Unit unit, LSFCastCmd cmd)
        {

        }
        public override bool Check(GameRoomComponent room, CastComponent component, LSFCastCmd cmd)
        {
            return true;
        }
        public override void RollBack(GameRoomComponent room, CastComponent component, LSFCastCmd cmd)
        {

        }
    }
}
