using MongoDB.Bson;

namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    [FriendOf(typeof(GameRoomComponent))]
    public class C2M_FrameCmdHandler : AMActorLocationRpcHandler<Unit, C2M_FrameCmdReq, M2C_FrameCmdRes>
    {
        protected override async ETTask Run(Unit unit, C2M_FrameCmdReq request, M2C_FrameCmdRes response)
        {
            /*foreach (Entity entity in ServerSceneManagerComponent.Instance.Children.Values)
            {
                Scene scene = entity as Scene;
                if (scene.SceneType != SceneType.Map) continue;
                Log.Warning($"{scene.Name} | {scene.SceneType} | {scene.DomainZone()}");
                Log.Warning(scene.GetComponent<UnitComponent>() == null);
                Log.Warning("------------------------------------------------");
            }*/
            /*Scene scene = unit.DomainScene();
            Log.Warning($"{scene.Name} | {scene.SceneType} | {scene.DomainZone()}");
            Log.Warning(scene.GetComponent<UnitComponent>() == null);*/
            
            GameRoomComponent room = Root.Instance.Scene.GetComponent<GameRoomComponent>();
            room.IsStart = true;
            room.TryAddSync(unit);
            LSFComponent lsf = unit.GetComponent<LSFComponent>();
            lsf.AddToReceive(request.Cmd);

            response.Frame = room.Frame;
            
            await ETTask.CompletedTask;
        }
    }
}