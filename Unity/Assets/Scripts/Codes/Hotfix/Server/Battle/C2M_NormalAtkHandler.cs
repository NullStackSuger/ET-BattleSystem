namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_NormalAtkHandler : AMActorLocationRpcHandler<Unit, C2M_NormalAtk, M2C_NormalAtk>
    {
        protected override async ETTask Run(Unit unit, C2M_NormalAtk request, M2C_NormalAtk response)
        {
            unit.GetComponent<CastComponent>().Creat(request.CastConfigId);
            await ETTask.CompletedTask;
        }
    }
}