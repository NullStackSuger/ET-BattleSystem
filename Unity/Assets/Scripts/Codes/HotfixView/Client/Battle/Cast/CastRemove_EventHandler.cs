using ET.EventType;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class CastRemove_EventHandler : AEvent<EventType.CastRemove>
    {
        protected override async ETTask Run(Scene scene, CastRemove a)
        {
            Log.Info("ClientView Event: Cast Remove");
            await ETTask.CompletedTask;
        }
    }
}