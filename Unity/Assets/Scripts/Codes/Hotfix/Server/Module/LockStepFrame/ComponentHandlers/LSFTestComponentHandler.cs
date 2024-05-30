namespace ET.Server
{
    [LSFComponentHandler(typeof(LSFTestCmd))]
    [FriendOfAttribute(typeof(ET.Server.LSFComponent))]
    public class LSFTestComponentHandler : LSFComponentHandler
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