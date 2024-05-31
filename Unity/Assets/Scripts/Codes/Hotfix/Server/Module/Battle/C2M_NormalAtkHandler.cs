namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_NormalAtkHandler : AMActorLocationRpcHandler<Unit, C2M_NormalAtk, M2C_NormalAtk>
    {
        protected override async ETTask Run(Unit unit, C2M_NormalAtk request, M2C_NormalAtk response)
        {
            /*TreeComponent tree = unit.AddChild<TreeComponent, string>("Server Tree Graph");
            tree.Start().Coroutine();*/

            //unit.GetComponent<CastComponent>().Creat(0);
            
            unit.AddComponent<TestComponent>();
            GameRoomComponent room = Root.Instance.Scene.GetComponent<GameRoomComponent>();
            room.TryAddSync(unit);
            await ETTask.CompletedTask;
        }
    }
}