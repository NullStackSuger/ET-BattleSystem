// 由Creat LSFCmd Editor生成
namespace ET.Client
{
    [LSFHandler(typeof(TreeComponent), typeof(LSFTreeCmd))]
    [FriendOf(typeof(GameRoomComponent))]
    public class LSFTreeHandler: LSFHandler<TreeComponent, LSFTreeCmd>
    {
        public override void TickStart(GameRoomComponent room, TreeComponent component, bool inRollBack)
        {

        }
        public override void Tick(GameRoomComponent room, TreeComponent component, bool inRollBack)
        {

        }
        public override void TickEnd(GameRoomComponent room, TreeComponent component, bool inRollBack)
        {

        }

        public override void Receive(Unit unit, LSFTreeCmd cmd)
        {

        }
        public override bool Check(GameRoomComponent room, TreeComponent component, LSFTreeCmd cmd)
        {
            return true;
        }
        public override void RollBack(GameRoomComponent room, TreeComponent component, LSFTreeCmd cmd)
        {

        }
    }
}
