namespace ET.Client
{
    [LSFComponentHandler(typeof(TestComponent))]
    [FriendOfAttribute(typeof(ET.Client.LSFComponent))]
    public class LSFTestComponentHandler : LSFComponentHandler
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
            return true;
        }

        public override void RollBack(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            
        }
    }
}