using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class BuffTick_EventHandler : AEvent<EventType.BuffTick>
    {
        protected override async ETTask Run(Scene scene, BuffTick a)
        {
            Log.Info("ClientView Event: Buff Tick");
            await ETTask.CompletedTask;
        }
    }
}