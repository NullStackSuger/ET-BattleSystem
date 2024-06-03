namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    [FriendOf(typeof(GameRoomComponent))]
    public class M2C_FrameCmdHandler : AMHandler<M2C_FrameCmd>
    {
        protected override async ETTask Run(Session session, M2C_FrameCmd message)
        {
            GameRoomComponent room = Root.Instance.Scene.GetComponent<GameRoomComponent>();
            //Log.Warning($"Client Frame: {room.Frame} | Server Frame: {message.Cmd.Frame}");
            UnitComponent unitComponent = session.ClientScene().GetComponent<UnitComponent>();
            /*Unit unit = unitComponent.Get(message.Cmd.UnitId);
            LSFComponent lsf = unit.GetComponent<LSFComponent>();
            lsf.AddToReceive(message.Cmd);*/
            await ETTask.CompletedTask;
        }
    }
}