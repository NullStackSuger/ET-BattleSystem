using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BuffRemove_EventHandler : AEvent<EventType.BuffRemove>
    {
        protected override async ETTask Run(Scene scene, BuffRemove a)
        {
            Log.Info("ClientView Event: Buff Remove");
            await ETTask.CompletedTask;
        }
    }
}