namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_BuffTickHandler : AMHandler<M2C_BuffTick>
    {
        protected override async ETTask Run(Session session, M2C_BuffTick message)
        {
            Log.Info("Client Message: Buff Tick");
            await EventSystem.Instance.PublishAsync(session.DomainScene(), new EventType.BuffTick());
        }
    }
}