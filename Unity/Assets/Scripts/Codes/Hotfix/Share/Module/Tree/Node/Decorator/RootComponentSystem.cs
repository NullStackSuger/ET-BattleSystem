namespace ET
{

    public static class RootComponentSystem
    {
        public class RootComponentAwakeSystem : AwakeSystem<RootComponent>
        { 
            protected override void Awake(RootComponent self)
            {
                
            }
        }
        
        [NodeRun(typeof(SelectorComponent))]
        public class RootComponentNodeRunSystem : NodeRun
        {
            public override async ETTask<bool> Run(Entity self, TreeComponent tree)
            {
                // 做初始化工作(TreeComponent)
                // 调用Child.NodeStart, 返回true
                await ETTask.CompletedTask;
                return false;
            }
        }
        
        public class RootComponentDestroySystem : DestroySystem<RootComponent>
        {
            protected override void Destroy(RootComponent self)
            {
                
            }
        }
    }
}