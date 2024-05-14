namespace ET.Client
{
    [FriendOf(typeof (BuffComponent))]
    [FriendOf(typeof (Buff))]
    public static class BuffComponentSystem
    {
        public class BuffComponentAwakeSystem: AwakeSystem<BuffComponent>
        {
            protected override void Awake(BuffComponent self)
            {
                self.Buffs?.Clear();
                self.Buffs ??= new();
            }
        }
        
        public class BuffComponentUpdateSystem : UpdateSystem<BuffComponent>
        {
            protected override void Update(BuffComponent self)
            {
                foreach (Buff buff in self.Buffs.Values)
                {
                    EventSystem.Instance.Publish(buff.DomainScene(), new EventType.BuffTick());
                }
            }
        }

        public class BuffComponentDestroySystem: DestroySystem<BuffComponent>
        {
            protected override void Destroy(BuffComponent self)
            {
                self.Buffs.Clear();
            }
        }

        public static Buff Creat(this BuffComponent self, int configId)
        {
            // 先判断Buff类型
            // if ( Buff is 叠层数的 ) 叠层数.Count++;
            // if ( Buff is 持续时间的 ) 持续时间 = 持续时间;
            // if ( Buff is 使用更大数值的 ) if ( Buff.Value > CurBuff.Value ) CurBuff = Buff;
            // ...
            
            // 创建Buff
            Buff buff = self.AddChild<Buff, int>(configId);
            self.Buffs.Add((int)buff.Id, buff);
            
            // buff的其他属性赋值
            buff.Owner = self.GetParent<Unit>();
            
            // 发送事件
            EventSystem.Instance.Publish(buff.DomainScene(), new EventType.BuffCreat());
            
            return buff;
        }

        public static void Remove(this BuffComponent self, int configId)
        {
            Buff buff = Get(self, configId);
            self.Buffs.Remove(configId);
            self.RemoveChild(buff.Id);
            
            // 发送事件
            EventSystem.Instance.Publish(buff.DomainScene(), new EventType.BuffRemove());
        }

        public static Buff Get(this BuffComponent self, int configId)
        {
            return self.Buffs[configId];
        }
    }
}