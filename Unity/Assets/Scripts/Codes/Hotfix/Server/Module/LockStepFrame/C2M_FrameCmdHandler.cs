namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    public class C2M_FrameCmdHandler : AMActorLocationHandler<Unit, C2M_FrameCmd>
    { 
        protected override async ETTask Run(Unit entity, C2M_FrameCmd message)
        {
            Log.Warning("C2M_FrameCmd");
            await ETTask.CompletedTask;
        }
    }
}