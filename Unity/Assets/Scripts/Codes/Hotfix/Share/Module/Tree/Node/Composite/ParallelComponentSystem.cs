namespace ET
{

    public static class ParallelComponentSystem
    {
        public class ParallelComponentAwakeSystem : AwakeSystem<ParallelComponent>
        { 
            protected override void Awake(ParallelComponent self)
            {
                
            }
        }
        
        [NodeRun(typeof(ParallelComponent))]
        public class ParallelComponentNodeRunSystem : NodeRun
        {
            public override async ETTask<bool> Run(Entity self, TreeComponent tree)
            {
                // 获取Chindren, 不await调用对应NodeStart, 返回true
                
                await ETTask.CompletedTask;
                return false;
            }
        }
        
        public class ParallelComponentDestroySystem : DestroySystem<ParallelComponent>
        {
            protected override void Destroy(ParallelComponent self)
            {
                
            }
        }
    }
}