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

            response.Frame = room.Frame;
            
            await ETTask.CompletedTask;
        }
    }
}