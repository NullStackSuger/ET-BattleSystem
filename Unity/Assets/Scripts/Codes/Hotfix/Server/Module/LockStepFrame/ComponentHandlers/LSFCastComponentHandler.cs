// 由Creat LSFCmd Editor生成
namespace ET.Server
{
    [LSFComponentHandler(typeof(CastComponent))]
    [FriendOf(typeof(LSFComponent))]
    public class LSFCastComponentHandler : LSFComponentHandler
    {
        public override void TickStart(GameRoomComponent room, Entity component)
        {
            Log.Info("Server.TickStart");
        }
        public override void Tick(GameRoomComponent room, Entity component)
        {
            Log.Info("Server.Tick");
        }
        public override void TickEnd(GameRoomComponent room, Entity component)
        {
            Log.Info("Server.TickEnd");
        }
    }
}
