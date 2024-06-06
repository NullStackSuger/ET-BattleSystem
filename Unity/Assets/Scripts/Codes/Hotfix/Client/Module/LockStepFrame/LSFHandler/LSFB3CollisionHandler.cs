// 由Creat LSFCmd Editor生成
namespace ET.Client
{
    [LSFHandler(typeof(B3CollisionComponent), typeof(LSFB3CollisionCmd))]
    [FriendOf(typeof(GameRoomComponent))]
    public class LSFB3CollisionHandler: LSFHandler<B3CollisionComponent, LSFB3CollisionCmd>
    {
        public override void TickStart(GameRoomComponent room, B3CollisionComponent component, bool inRollBack)
        {

        }
        public override void Tick(GameRoomComponent room, B3CollisionComponent component, bool inRollBack)
        {

        }
        public override LSFCmd TickEnd(GameRoomComponent room, B3CollisionComponent component, bool inRollBack)
        {
            return null;
        }

        public override void Receive(Unit unit, B3CollisionComponent component, LSFB3CollisionCmd cmd)
        {

        }

        public override bool Check(LSFB3CollisionCmd clientCmd, LSFB3CollisionCmd serverCmd)
        {
            return true;
        }

        public override void RollBack(GameRoomComponent room, B3CollisionComponent component, LSFB3CollisionCmd cmd)
        {

        }
    }
}
