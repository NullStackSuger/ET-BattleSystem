using ET.Server;

namespace ET.Server
{

    public static class CastSystem
    {
        public class CastAwakeSystem: AwakeSystem<Cast, int>
        {
            protected override void Awake(Cast self, int id)
            {
                self.ConfigId = id;
                self.Targets?.Clear();
                self.Targets ??= new();
                
                ActionComponent actionComponent = self.AddComponent<ActionComponent>();
                
                // 根据不同范围检测类型添加不同组件
                // 抽象出个基类, 用的时候调用.Selected()
                switch (CastConfigCategory.Instance.Get(self.ConfigId).SelectType)
                {
                    case 0:
                        break;
                    case 1:
                        break;
                    default:
                        break;
                }

                // 添加行为
                CastConfig castConfig = CastConfigCategory.Instance.Get(id);
                foreach (int actionId in castConfig.ActionIds)
                {
                    actionComponent.Creat(actionId);
                }
            }
        }
        
        public class CastDestorySystem : DestroySystem<Cast>
        {
            protected override void Destroy(Cast self)
            {
                self.ConfigId = default;
                self.Owner = default;
                self.Targets.Clear();
            }
        }
        
        public class CastUpdateSystem : UpdateSystem<Cast>
        {
            protected override void Update(Cast self)
            {
                M2C_CastTick message = new()
                {
                    CastId = self.Id,
                    CasterId = self.Owner.Id,
                    TargetsId = new(self.Targets),
                };

                Unit unit = self.Parent.GetParent<Unit>();
                NoticeClientHelper.Send(unit, message, 
                    (NoticeClientType)CastConfigCategory.Instance.Get(self.ConfigId).NoticeClientType);
            }
        }
    }
}