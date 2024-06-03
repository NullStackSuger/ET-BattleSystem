// 由Creat LSFCmd Editor生成

using Unity.Mathematics;

namespace ET.Client
{
    [LSFComponentHandler(typeof(MoveComponent))]
    [FriendOf(typeof(LSFComponent))]
    [FriendOf(typeof(GameRoomComponent))]
    public class LSFMoveComponentHandler : LSFComponentHandler
    {
        public override void TickStart(GameRoomComponent room, Entity component, bool needSend)
        {
        }
        public override void Tick(GameRoomComponent room, Entity component, bool needSend)
        {
        }
        public override void TickEnd(GameRoomComponent room, Entity component, bool needSend)
        {
            Unit unit = component.GetParent<Unit>();
            LSFMoveCmd moveCmd = new()
            {
                Frame = room.Frame,
                Position = unit.Position,
                Rotation = unit.Rotation,
            };
            LSFComponent lsf = unit.GetComponent<LSFComponent>();
            lsf.AddToSend(moveCmd);
        }
        public override bool Check(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            Log.Info("Client.Check MoveComponent");
            LSFMoveCmd moveCmd = cmd as LSFMoveCmd;
            Unit unit = component.GetParent<Unit>();
            if (math.distance(moveCmd.Position, unit.Position) < 1.0 && unit.Rotation.Equals(moveCmd.Rotation))
            {
                return true;
            }
            else
            {
                Log.Warning("Move UnPassCheck");
                return false;
            }
        }
        public override void RollBack(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            Log.Info("Client.RollBack");
            Unit unit = component.GetParent<Unit>();
            LSFMoveCmd moveCmd = cmd as LSFMoveCmd;
            unit.Position = moveCmd.Position;
            unit.Rotation = moveCmd.Rotation;
        }
    }
}
