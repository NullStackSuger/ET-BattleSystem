namespace ET
{

    public static class BuffSystem
    {
        public class BuffAwakeSystem: AwakeSystem<Buff, int>
        {
            protected override void Awake(Buff self, int id)
            {
                self.ConfigId = id;
                self.Targets?.Clear();
                self.Targets ??= new();
                
                ActionComponent actionComponent = self.AddComponent<ActionComponent>();
                
                // 根据不同范围检测类型添加不同组件
                // 抽象出个基类, 用的时候调用.Selected()
                switch (BuffConfigCategory.Instance.Get(self.ConfigId).SelectType)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    default:
                        break;
                }

                // 添加行为
                BuffConfig buffConfig = BuffConfigCategory.Instance.Get(id);
                /*foreach (string tree in buffConfig.Actions)
                {
                    if (tree == "") continue;
                    actionComponent.Creat(tree);
                }*/
            }
        }
        
        public class BuffDestroySystem : DestroySystem<Buff>
        {
            protected override void Destroy(Buff self)
            {
                self.ConfigId = default;
                self.Owner = default;
                self.Targets.Clear();
            }
        }
    }
}