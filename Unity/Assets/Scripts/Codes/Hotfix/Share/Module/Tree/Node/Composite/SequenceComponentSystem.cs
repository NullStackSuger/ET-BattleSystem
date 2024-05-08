namespace ET
{

    public static class SequenceComponentSystem
    {
        public class SequenceComponentAwakeSystem : AwakeSystem<SequenceComponent>
        { 
            protected override void Awake(SequenceComponent self)
            {
                
            }
        }
        
        [NodeRun(typeof(SelectorComponent))]
        public class SequenceComponentNodeRunSystem : NodeRun
        { 
            public override async ETTask<bool> Run(Entity self, TreeComponent tree)
            {
                // 获取Chindren, await调用对应NodeStart, 当有false, break并返回false, 否则返回true
                await ETTask.CompletedTask;
                return false;
            }
        }
        
        public class SequenceComponentDestroySystem : DestroySystem<SequenceComponent>
        {
            protected override void Destroy(SequenceComponent self)
            {
                
            }
        }
    }
}