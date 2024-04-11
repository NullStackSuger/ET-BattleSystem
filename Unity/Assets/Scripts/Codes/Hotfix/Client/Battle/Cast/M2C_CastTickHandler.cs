namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_CastTickHandler : AMHandler<M2C_CastTick>
    {
        protected override async ETTask Run(Session session, M2C_CastTick message)
        {
            Log.Info("Client Message: Cast Tick");
            await EventSystem.Instance.PublishAsync(session.DomainScene(), new EventType.CastTick());
        }
    }
}