namespace ET
{

    public static class LogComponentSystem
    {
        public class LogComponentAwakeSystem: AwakeSystem<LogComponent>
        {
            protected override void Awake(LogComponent self)
            {
                
            }
        }
        
        [NodeRun(typeof(SelectorComponent))]
        public class LogComponentNodeRunSystem : NodeRun
        {
            public override async ETTask<bool> Run(Entity self, TreeComponent tree)
            {
                Log.Info("LogNode");
                await ETTask.CompletedTask;
                return false;
            }
        }
        
        public class LogComponentDestroySystem : DestroySystem<LogComponent>
        {
            protected override void Destroy(LogComponent self)
            {
                
            }
        }
    }
}