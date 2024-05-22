
using MongoDB.Bson.Serialization;

namespace ET.Client
{
    [Event(SceneType.Process)]
    public class EntryEvent3_InitClient: AEvent<ET.EventType.EntryEvent3>
    {
        protected override async ETTask Run(Scene scene, ET.EventType.EntryEvent3 args)
        {
            // 加载配置
            Root.Instance.Scene.AddComponent<ResourcesComponent>();
            
            Root.Instance.Scene.AddComponent<GlobalComponent>();
            
            Root.Instance.Scene.AddComponent<NodeDispatcherComponent>();
            
            Root.Instance.Scene.AddComponent<B2CollisionHandlerDispatcherComponent>();
            
            // 正常是一个房间一个World, 但这里Demo当作直接开启一局了
            Root.Instance.Scene.AddComponent<B2WorldComponent>();

            await ResourcesComponent.Instance.LoadBundleAsync("unit.unity3d");
            
            Scene clientScene = await SceneFactory.CreateClientScene(1, "Game");
            
            await EventSystem.Instance.PublishAsync(clientScene, new EventType.AppStartInitFinish());
            
            Log.Info("开始测试");
            
            /*ET.Node.Root tree = TreeFactory.Creat("C Tree Graph", null);
            tree.Start();*/
        }
    }
}