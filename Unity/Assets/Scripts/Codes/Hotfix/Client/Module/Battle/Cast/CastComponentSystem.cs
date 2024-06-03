using Unity.Mathematics;

namespace ET.Client
{
    [FriendOf(typeof(CastComponent))]
    [FriendOf(typeof(Cast))]
    public static class CastComponentSystem
    {
        public class CastComponentAwakeSystem : AwakeSystem<CastComponent>
        {
            protected override void Awake(CastComponent self)
            {
                self.Casts?.Clear();
                self.Casts ??= new();
            }
        }
        
        public class CastComponentUpdateSystem : UpdateSystem<CastComponent>
        {
            protected override void Update(CastComponent self)
            {
                foreach (Cast cast in self.Casts)
                {
                    EventSystem.Instance.Publish(cast.DomainScene(), new EventType.CastTick());
                }
            }
        }

        public class CastComponentDestroySystem : DestroySystem<CastComponent>
        {
            protected override void Destroy(CastComponent self)
            {
                self.Casts.Clear();
            }
        }

        public static Unit Creat(this CastComponent self, int configId)
        {
            // 创建技能 
            Unit castUnit = UnitFactory.CreatCast(self.DomainScene(), configId, float3.zero, quaternion.identity);
            Cast cast = castUnit.GetComponent<Cast>();
            self.Casts.Add(cast);

            // Cast的其他属性赋值
            cast.Owner = self.GetParent<Unit>();
            
            // 发送事件
            EventSystem.Instance.Publish(cast.DomainScene(), new EventType.CastCreat() { CastId = cast.Id });

            return castUnit;
        }

        public static void Remove(this CastComponent self, int id)
        {
            Cast cast = Get(self, id);
            self.Casts.Remove(cast);
            self.RemoveChild(cast.Id);
            
            // 发送事件
            EventSystem.Instance.Publish(cast.DomainScene(), new EventType.CastRemove());
        }

        public static Cast Get(this CastComponent self, int id)
        {
            foreach (Cast cast in self.Casts)
            {
                if (cast.Id == id)
                {
                    return cast;
                }
            }

            return null;
        }
    }
}