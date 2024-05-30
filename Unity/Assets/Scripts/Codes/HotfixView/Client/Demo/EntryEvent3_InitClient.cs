
using BulletSharp;
using BulletSharp.Math;
using BulletUnity;
using MongoDB.Bson.Serialization;

namespace ET.Client
{
    [Event(SceneType.Process)]
    [FriendOfAttribute(typeof(ET.B3CollisionComponent))]
    public class EntryEvent3_InitClient : AEvent<ET.EventType.EntryEvent3>
    {
        protected override async ETTask Run(Scene scene, ET.EventType.EntryEvent3 args)
        {
            // 加载配置
            Root.Instance.Scene.AddComponent<ResourcesComponent>();

            Root.Instance.Scene.AddComponent<GlobalComponent>();

            Root.Instance.Scene.AddComponent<NodeDispatcherComponent>();

            B3WorldComponent world = Root.Instance.Scene.AddComponent<B3WorldComponent>();

            await ResourcesComponent.Instance.LoadBundleAsync("unit.unity3d");

            Scene clientScene = await SceneFactory.CreateClientScene(1, "Game");

            await EventSystem.Instance.PublishAsync(clientScene, new EventType.AppStartInitFinish());

            Log.Info("开始测试");
        }
    }
}