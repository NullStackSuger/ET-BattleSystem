namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_CastCreatHandler : AMHandler<M2C_CastCreat>
    {
        protected override async ETTask Run(Session session, M2C_CastCreat message)
        {
            Log.Info("Client Message: Cast Creat");
            await EventSystem.Instance.PublishAsync(session.DomainScene(), new EventType.CastCreat(){ CastId = message.CastId });
        }
    }
}