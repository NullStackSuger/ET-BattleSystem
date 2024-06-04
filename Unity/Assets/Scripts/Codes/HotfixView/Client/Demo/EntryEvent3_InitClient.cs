
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

            await ResourcesComponent.Instance.LoadBundleAsync("unit.unity3d");

            Scene clientScene = await SceneFactory.CreateClientScene(1, "Game");
            
            clientScene.AddComponent<GameRoomComponent>();
            
            var world = clientScene.AddComponent<B3WorldComponent>();

            await EventSystem.Instance.PublishAsync(clientScene, new EventType.AppStartInitFinish());

            Log.Info("开始测试");
            
            
        }
    }
}