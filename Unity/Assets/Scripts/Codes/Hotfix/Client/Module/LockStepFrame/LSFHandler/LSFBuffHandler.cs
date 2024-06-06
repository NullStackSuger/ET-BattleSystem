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
        public override LSFCmd TickEnd(GameRoomComponent room, BuffComponent component, bool inRollBack)
        {
            return null;
        }

        public override void Receive(Unit unit, BuffComponent component, LSFBuffCmd cmd)
        {

        }
        
        public override bool Check(LSFBuffCmd clientCmd, LSFBuffCmd serverCmd)
        {
            return true;
        }
        
        public override void RollBack(GameRoomComponent room, BuffComponent component, LSFBuffCmd cmd)
        {

        }
    }
}
