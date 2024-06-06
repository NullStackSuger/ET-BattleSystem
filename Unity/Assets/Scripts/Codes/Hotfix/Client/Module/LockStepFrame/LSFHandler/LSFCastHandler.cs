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
        public override LSFCmd TickEnd(GameRoomComponent room, CastComponent component, bool inRollBack)
        {
            return null;
        }

        public override void Receive(Unit unit, CastComponent component, LSFCastCmd cmd)
        {

        }

        public override bool Check(LSFCastCmd clientCmd, LSFCastCmd serverCmd)
        {
            return true;
        }

        public override void RollBack(GameRoomComponent room, CastComponent component, LSFCastCmd cmd)
        {

        }
    }
}
