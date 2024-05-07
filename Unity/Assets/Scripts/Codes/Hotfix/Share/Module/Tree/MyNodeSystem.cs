using ET.Server;

namespace ET
{

    public static class MyNodeSystem
    {
        public class MyNodeAwakeSystem : AwakeSystem<MyNode>
        {
            protected override void Awake(MyNode self)
            {
                
            }
        }
        public class MyNodeStartSystem : NodeSystem<MyNode, TreeComponent>
        {
            protected override async void NodeStart(MyNode self, TreeComponent a)
            {
                // 这个应该放在Server, 这里就是测试
                a.Unit.GetComponent<CastComponent>().Creat(0001);
                
                await ETTask.CompletedTask;
            }

            protected override void NodeStop(MyNode self)
            {
                
            }
        }
    }
}