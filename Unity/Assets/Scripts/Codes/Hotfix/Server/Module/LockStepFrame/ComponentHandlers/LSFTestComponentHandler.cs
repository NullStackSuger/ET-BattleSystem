// 由Creat LSFCmd Editor生成
namespace ET.Server
{
    [LSFComponentHandler(typeof(TestComponent))]
    [FriendOf(typeof(LSFComponent))]
    public class LSFTestComponentHandler : LSFComponentHandler
    {
        public override void TickStart(GameRoomComponent room, Entity component)
        {
            Log.Info("Server.TickStart TestComponent");
        }
        public override void Tick(GameRoomComponent room, Entity component)
        {
            Log.Info("Server.Tick TestComponent");
        }
        public override void TickEnd(GameRoomComponent room, Entity component)
        {
            Log.Info("Server.TickEnd TestComponent");
        }
    }
}
