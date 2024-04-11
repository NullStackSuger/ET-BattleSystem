namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_CastRemoveHandler : AMHandler<M2C_CastRemove>
    {
        protected override async ETTask Run(Session session, M2C_CastRemove message)
        {
            Log.Info("Client Message: Cast Remove");
            await EventSystem.Instance.PublishAsync(session.DomainScene(), new EventType.CastRemove());
        }
    }
}