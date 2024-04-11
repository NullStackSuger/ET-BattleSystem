namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_BuffRemoveHandler : AMHandler<M2C_BuffRemove>
    {
        protected override async ETTask Run(Session session, M2C_BuffRemove message)
        {
            Log.Info("Client Message: Buff Remove");
            await EventSystem.Instance.PublishAsync(session.DomainScene(), new EventType.BuffRemove());
        }
    }
}