using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BuffCreat_EventHandler : AEvent<EventType.BuffCreat>
    {
        protected override async ETTask Run(Scene scene, BuffCreat a)
        {
            Log.Info("ClientView Event: Buff Creat");
            await ETTask.CompletedTask;
        }
    }
}