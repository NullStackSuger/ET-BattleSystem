using Unity.Mathematics;

namespace ET.Server
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

        public class CastComponentDestroySystem : DestroySystem<CastComponent>
        {
            protected override void Destroy(CastComponent self)
            {
                self.Casts.Clear();
            }
        }
        
        [FriendOf(typeof(Cast))]
        public class CastComponentUpdateSystem : UpdateSystem<CastComponent>
        {
            protected override void Update(CastComponent self)
            {
                M2C_CastTick message = new() { CastId = self.Id, };

                foreach (Cast cast in self.Casts)
                {
                    message.CasterId = cast.Owner.Id;
                    message.TargetsId = cast.Targets;
                    
                    NoticeClientHelper.Send(cast.Owner, message,
                        (NoticeClientType)CastConfigCategory.Instance.Get(cast.ConfigId).NoticeClientType);
                }
            }
        }

        public static Cast Creat(this CastComponent self, int configId, M2C_CastCreat message = null)
        {
            // 创建技能 
            Unit castUnit = UnitFactory.CreatCast(self.DomainScene(), configId, float3.zero, quaternion.identity);
            Cast cast = castUnit.GetComponent<Cast>();
            self.Casts.Add(cast);

            // Cast的其他属性赋值
            cast.Owner = self.GetParent<Unit>();
            // 向客户端发送消息
            message ??= new();
            message.CastId = cast.Id;
            message.CasterId = cast.Owner.Id;
            NoticeClientHelper.Send(cast.Owner, message, 
                (NoticeClientType)CastConfigCategory.Instance.Get(configId).NoticeClientType);
            Log.Warning("SendMessageToClient");

            return cast;
        }

        public static void Remove(this CastComponent self, int id, M2C_CastRemove message = null)
        {
            Cast cast = Get(self, id);
            self.Casts.Remove(cast);
            self.RemoveChild(cast.Id);
            
            // 向客户端发送消息
            message ??= new();
            message.CastId = cast.Id;
            message.CasterId = cast.Owner.Id;
            NoticeClientHelper.Send(self.GetParent<Unit>(), message, 
                (NoticeClientType)CastConfigCategory.Instance.Get(cast.ConfigId).NoticeClientType);
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