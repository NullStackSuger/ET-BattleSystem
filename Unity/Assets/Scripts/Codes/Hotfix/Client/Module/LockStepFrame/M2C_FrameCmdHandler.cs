namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    [FriendOf(typeof(GameRoomComponent))]
    public class M2C_FrameCmdHandler : AMHandler<M2C_FrameCmd>
    {
        protected override async ETTask Run(Session session, M2C_FrameCmd message)
        {
            GameRoomComponent room = session.DomainScene().GetComponent<GameRoomComponent>();
            room.AddToReceive(message.Cmd);
            await ETTask.CompletedTask;
        }
    }
}