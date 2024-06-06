using MongoDB.Bson;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    [FriendOf(typeof(GameRoomComponent))]
    public class C2M_FrameCmdHandler : AMActorLocationRpcHandler<Unit, C2M_FrameCmdReq, M2C_FrameCmdRes>
    {
        protected override async ETTask Run(Unit unit, C2M_FrameCmdReq request, M2C_FrameCmdRes response)
        {
            GameRoomComponent room = unit.DomainScene().GetComponent<GameRoomComponent>();
            room.IsStart = true;
            room.TryAddSync(unit);
            room.AddToReceive(request.Cmd);
            
            Log.Warning($"Server 在第{room.Frame}帧收到客户端第{request.Cmd.Frame}帧消息 | 此时服务端还有{room.Receives.Count}条消息未处理, {room.Sends.Count}条消息未发送");

            response.Frame = room.Frame;
            
            await ETTask.CompletedTask;
        }
    }
}