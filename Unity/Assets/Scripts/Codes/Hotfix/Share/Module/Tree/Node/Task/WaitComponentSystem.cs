namespace ET
{

    public static class WaitComponentSystem
    {
        public class WaitComponentAwakeSystem: AwakeSystem<WaitComponent>
        {
            protected override void Awake(WaitComponent self)
            {
                
            }
        }
        
        [NodeRun(typeof(SelectorComponent))]
        public class WaitComponentNodeRunSystem : NodeRun
        {
            public override async ETTask<bool> Run(Entity self, TreeComponent tree)
            {
                await ETTask.CompletedTask;
                return false;
            }
        }
        
        public class WaitComponentDestroySystem : DestroySystem<WaitComponent>
        {
            protected override void Destroy(WaitComponent self)
            {
                
            }
        }
    }
}