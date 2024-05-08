namespace ET
{

    public static class SelectorComponentSystem
    {
        public class SelectorComponentAwakeSystem : AwakeSystem<SelectorComponent>
        { 
            protected override void Awake(SelectorComponent self)
            {
                
            }
        }
        
        [NodeRun(typeof(SelectorComponent))]
        public class SelectorComponentNodeRunSystem : NodeRun
        {
            public override async ETTask<bool> Run(Entity self, TreeComponent tree)
            {
                // 获取Chindren, await调用对应NodeStart, 当有true, break并返回true, 否则返回false
                await ETTask.CompletedTask;
                return false;
            }
        }
        
        public class SelectorComponentDestroySystem : DestroySystem<SelectorComponent>
        {
            protected override void Destroy(SelectorComponent self)
            {
                
            }
        }
    }
}