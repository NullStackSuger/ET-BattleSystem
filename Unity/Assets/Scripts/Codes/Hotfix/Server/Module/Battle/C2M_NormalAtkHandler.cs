namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_NormalAtkHandler : AMActorLocationRpcHandler<Unit, C2M_NormalAtk, M2C_NormalAtk>
    {
        protected override async ETTask Run(Unit unit, C2M_NormalAtk request, M2C_NormalAtk response)
        {
            //ET.Node.Root tree = TreeFactory.Creat("S Tree Graph", unit);
            //tree.Start();
            //unit.GetComponent<CastComponent>().Creat(request.CastConfigId);
            unit.AddComponent<TreeComponent, string>("S Tree Graph");
            await ETTask.CompletedTask;
        }
    }
}