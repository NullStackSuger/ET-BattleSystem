using ET.EventType;
using UnityEngine;

namespace ET.Client
{
    [Event(SceneType.Client)]
    public class CastCreat_EventHandler : AEvent<EventType.CastCreat>
    {
        protected override async ETTask Run(Scene scene, CastCreat a)
        {
            await ResourcesComponent.Instance.LoadBundleAsync("test.unity3d");
            GameObject prefabe = (GameObject)ResourcesComponent.Instance.GetAsset("test.unity3d", "test");
            GameObject obj = UnityEngine.Object.Instantiate(prefabe, GlobalComponent.Instance.Unit, false);
            Log.Warning("Creat Prefabe: " + obj.name);
            await ETTask.CompletedTask;
        }
    }
}