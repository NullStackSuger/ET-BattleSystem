
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
            
            //Root.Instance.Scene.AddComponent<ActionsDispatcherComponent>();

            await ResourcesComponent.Instance.LoadBundleAsync("unit.unity3d");
            
            Scene clientScene = await SceneFactory.CreateClientScene(1, "Game");
            
            await EventSystem.Instance.PublishAsync(clientScene, new EventType.AppStartInitFinish());
            
            Log.Info("开始测试");

            // 找到问题了, 但是不知道怎么改
            // MongoHelper
            BsonClassMap.RegisterClassMap<CTestNode>();
            
            NPBehave.Root tree = TreeFactory.Creat("C Tree Graph", null);
            Log.Info("Tree: " + tree == null);
            tree.Start();

            /*NPBehave.Root tree = new(new CTestNode());
            tree.Start();*/
        }
    }
}