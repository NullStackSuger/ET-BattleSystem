using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class CastTick_EventHandler : AEvent<EventType.CastTick>
    {
        protected override async ETTask Run(Scene scene, CastTick a)
        { 
            Log.Info("ClientView Event: Cast Tick");
            await ETTask.CompletedTask;
        }
    }
}