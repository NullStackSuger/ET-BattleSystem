// 由Creat LSFCmd Editor生成
namespace ET.Server
{
    [LSFComponentHandler(typeof(MoveComponent))]
    [FriendOf(typeof(LSFComponent))]
    [FriendOf(typeof(GameRoomComponent))]
    public class LSFMoveComponentHandler : LSFComponentHandler
    {
        public override void TickStart(GameRoomComponent room, Entity component)
        {
        }
        public override void Tick(GameRoomComponent room, Entity component)
        {
        }
        public override void TickEnd(GameRoomComponent room, Entity component)
        {
            Unit unit = component.GetParent<Unit>();
            LSFMoveCmd moveCmd = new()
            {
                Frame = room.Frame,
                UnitId = unit.Id,
                Position = unit.Position,
                Rotation = unit.Rotation,
            };
            LSFComponent lsf = unit.GetComponent<LSFComponent>();
            lsf.AddToSend(moveCmd);
        }
    }
}
