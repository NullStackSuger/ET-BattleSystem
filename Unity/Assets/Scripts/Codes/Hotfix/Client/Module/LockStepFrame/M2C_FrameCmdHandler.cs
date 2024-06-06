using MongoDB.Bson;

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
            Log.Warning($"Client 在第{room.Frame}帧收到服务端第{message.Cmd.Frame}帧消息 | 此时客户端还有{room.Receives.Count}条消息未处理, {room.Sends.Count}条消息未发送");
            await ETTask.CompletedTask;
        }
    }
}