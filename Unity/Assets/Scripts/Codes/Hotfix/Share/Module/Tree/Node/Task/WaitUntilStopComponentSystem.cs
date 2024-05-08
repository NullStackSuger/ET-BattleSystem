namespace ET
{

    public static class WaitUntilStopComponentSystem
    {
        public class WaitUntilStopComponentAwakeSystem: AwakeSystem<WaitUntilStopComponent>
        {
            protected override void Awake(WaitUntilStopComponent self)
            {
                
            }
        }
        
        [NodeRun(typeof(SelectorComponent))]
        public class WaitUntilStopComponentNodeRunSystem : NodeRun
        {
            public override async ETTask<bool> Run(Entity self, TreeComponent tree)
            {
                await ETTask.CompletedTask;
                return false;
            }
        }
        
        public class WaitUntilStopComponentDestroySystem : DestroySystem<WaitUntilStopComponent>
        {
            protected override void Destroy(WaitUntilStopComponent self)
            {
                
            }
        }
    }
}