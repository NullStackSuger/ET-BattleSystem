// 由Creat LSFCmd Editor生成
namespace ET.Client
{
    [LSFComponentHandler(typeof(TreeComponent))]
    [FriendOf(typeof(LSFComponent))]
    public class LSFTreeComponentHandler : LSFComponentHandler
    {
        public override void TickStart(GameRoomComponent room, Entity component, bool needSend)
        {
            Log.Info("Client.TickStart");
        }
        public override void Tick(GameRoomComponent room, Entity component, bool needSend)
        {
            Log.Info("Client.Tick");
        }
        public override void TickEnd(GameRoomComponent room, Entity component, bool needSend)
        {
            Log.Info("Client.TickEnd");
        }
        public override bool Check(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            Log.Info("Client.Check");
            return true;
        }
        public override void RollBack(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            Log.Info("Client.RollBack");
        }
    }
}
