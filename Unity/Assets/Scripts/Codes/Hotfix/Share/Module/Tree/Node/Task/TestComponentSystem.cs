using System;

namespace ET
{
    public static class TestComponentSystem
    {
        public class TestComponentAwakeSystem: AwakeSystem<TestComponent>
        {
            protected override void Awake(TestComponent self)
            {
                
            }
        }
        
        public class TestComponentDestroySystem : DestroySystem<TestComponent>
        {
            protected override void Destroy(TestComponent self)
            {
                
            }
        }
        
        public class TestComponentNodeRunSystem : NodeRunSystem<TestComponent>
        {
            public override async ETTask<bool> Run(TestComponent self)
            {
                EventSystem.Instance.GetSystem(self.GetType());
                await ETTask.CompletedTask;
                return true;
            }
        }
    }
}