namespace ET.Client
{
    [MessageHandler(SceneType.Client)]
    public class M2C_BuffCreatHandler : AMHandler<M2C_BuffCreat>
    {
        protected override async ETTask Run(Session session, M2C_BuffCreat message)
        {
            Log.Info("Client Message: Buff Creat");
            await EventSystem.Instance.PublishAsync(session.DomainScene(), new EventType.BuffCreat());
        }
    }
}