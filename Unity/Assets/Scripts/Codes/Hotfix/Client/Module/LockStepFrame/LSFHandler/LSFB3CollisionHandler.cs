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
        public override void TickEnd(GameRoomComponent room, B3CollisionComponent component, bool inRollBack)
        {

        }

        public override void Receive(Unit unit, LSFB3CollisionCmd cmd)
        {

        }
        public override bool Check(GameRoomComponent room, B3CollisionComponent component, LSFB3CollisionCmd cmd)
        {
            return true;
        }
        public override void RollBack(GameRoomComponent room, B3CollisionComponent component, LSFB3CollisionCmd cmd)
        {

        }
    }
}
