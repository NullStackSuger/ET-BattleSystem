// 由Creat LSFCmd Editor生成
namespace ET.Client
{
    [LSFHandler(typeof(BuffComponent), typeof(LSFBuffCmd))]
    [FriendOf(typeof(GameRoomComponent))]
    public class LSFBuffHandler: LSFHandler<BuffComponent, LSFBuffCmd>
    {
        public override void TickStart(GameRoomComponent room, BuffComponent component, bool inRollBack)
        {

        }
        public override void Tick(GameRoomComponent room, BuffComponent component, bool inRollBack)
        {

        }
        public override void TickEnd(GameRoomComponent room, BuffComponent component, bool inRollBack)
        {

        }

        public override void Receive(Unit unit, LSFBuffCmd cmd)
        {

        }
        public override bool Check(GameRoomComponent room, BuffComponent component, LSFBuffCmd cmd)
        {
            return true;
        }
        public override void RollBack(GameRoomComponent room, BuffComponent component, LSFBuffCmd cmd)
        {

        }
    }
}
