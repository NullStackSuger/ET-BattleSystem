
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

            /*NPBehave.Root tree = TreeFactory.BuildTree("C Tree Graph");
            Log.Info("Colck" + tree.Clock == null);
            Log.Info("Blackboard" + tree.Blackboard == null);
            tree.Start();*/

            /*NPBehave.Root tree = new(new CTestNode());
            tree.Start();
            Log.Info("Colck" + tree.Clock == null);
            Log.Info("Blackboard" + tree.Blackboard == null);*/
        }
    }
}