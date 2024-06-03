namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_UseCastHandler : AMActorLocationRpcHandler<Unit, C2M_UseCast, M2C_UseCast>
    {
        protected override async ETTask Run(Unit unit, C2M_UseCast request, M2C_UseCast response)
        {
            /*TreeComponent tree = unit.AddChild<TreeComponent, string>("Server Tree Graph");
            tree.Start().Coroutine();*/

            unit.GetComponent<CastComponent>().Creat(request.CastConfigId);
            
            /*unit.AddComponent<TestComponent>();
            GameRoomComponent room = Root.Instance.Scene.GetComponent<GameRoomComponent>();
            room.TryAddSync(unit);*/
            await ETTask.CompletedTask;
        }
    }
}