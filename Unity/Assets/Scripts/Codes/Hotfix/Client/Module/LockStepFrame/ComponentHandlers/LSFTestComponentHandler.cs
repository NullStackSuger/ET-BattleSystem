// 由Creat LSFCmd Editor生成
namespace ET.Client
{
    [LSFComponentHandler(typeof(TestComponent))]
    [FriendOf(typeof(LSFComponent))]
    public class LSFTestComponentHandler : LSFComponentHandler
    {
        public override void TickStart(GameRoomComponent room, Entity component, bool needSend)
        {
            Log.Info("Client.TickStart TestComponent");
        }
        public override void Tick(GameRoomComponent room, Entity component, bool needSend)
        {
            Log.Info("Client.Tick TestComponent");
        }
        public override void TickEnd(GameRoomComponent room, Entity component, bool needSend)
        {
            Log.Info("Client.TickEnd TestComponent");
        }
        public override bool Check(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            Log.Info("Client.Check TestComponent");
            return true;
        }
        public override void RollBack(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            Log.Info("Client.RollBack");
        }
    }
}
