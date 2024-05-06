using System.Collections.Generic;

namespace ET.Server
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

        public class BuffComponentDestorySystem: DestroySystem<BuffComponent>
        {
            protected override void Destroy(BuffComponent self)
            {
                self.Buffs.Clear();
            }
        }
        [FriendOf(typeof(Buff))]
        public class BuffComponentUpdateSystem : UpdateSystem<BuffComponent>
        {
            protected override void Update(BuffComponent self)
            {
                M2C_BuffTick message = new() { BuffId = self.Id, };

                foreach (Buff buff in self.Buffs.Values)
                {
                    message.CasterId = buff.Owner.Id;
                    message.TargetsId = buff.Targets;

                    Unit unit = buff.Parent.GetParent<Unit>();
                    NoticeClientHelper.Send(unit, message,
                        (NoticeClientType)BuffConfigCategory.Instance.Get(buff.ConfigId).NoticeClientType);
                }
            }
        }

        public static Buff Creat(this BuffComponent self, int configId, M2C_BuffCreat message = null)
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
            
            // 向客户端发送消息
            message ??= new();
            message.BuffId = buff.Id;
            message.CasterId = buff.Owner.Id;
            NoticeClientHelper.Send(buff.Owner, message, 
                (NoticeClientType)BuffConfigCategory.Instance.Get(configId).NoticeClientType);
            
            return buff;
        }

        public static void Remove(this BuffComponent self, int configId, M2C_BuffRemove message = null)
        {
            Buff buff = Get(self, configId);
            self.Buffs.Remove(configId);
            self.RemoveChild(buff.Id);
            
            // 向客户端发送消息
            message ??= new();
            message.BuffId = buff.Id;
            message.CasterId = buff.Owner.Id;
            NoticeClientHelper.Send(self.GetParent<Unit>(), message, 
                (NoticeClientType)BuffConfigCategory.Instance.Get(buff.ConfigId).NoticeClientType);
        }

        public static Buff Get(this BuffComponent self, int configId)
        {
            return self.Buffs[configId];
        }
    }
}