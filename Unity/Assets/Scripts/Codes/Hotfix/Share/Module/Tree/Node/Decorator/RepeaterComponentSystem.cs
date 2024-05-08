namespace ET
{

    public static class RepeaterComponentSystem
    {
        public class RepeaterComponentAwakeSystem : AwakeSystem<RepeaterComponent>
        { 
            protected override void Awake(RepeaterComponent self)
            {
                
            }
        }
        
        [NodeRun(typeof(SelectorComponent))]
        public class RepeaterComponentNodeRunSystem : NodeRun
        {
            public override async ETTask<bool> Run(Entity self, TreeComponent tree)
            {
                // 循环调用Child.NodeStart, 返回true
                await ETTask.CompletedTask;
                return false;
            }
        }
        
        public class RepeaterComponentDestroySystem : DestroySystem<RepeaterComponent>
        {
            protected override void Destroy(RepeaterComponent self)
            {
                
            }
        }
    }
}