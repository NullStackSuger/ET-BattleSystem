using ET.Server;
using Unity.Mathematics;

namespace ET.Server
{

    [Action(0000)]
    [FriendOf(typeof(Cast))]
    [FriendOfAttribute(typeof(ET.Server.Buff))]
    public class FireBallCastAction : IAction
    {
        public bool Check(ActionComponent actionComponent, ActionConfig config)
        {
            return true;
        }

        public async ETTask Run(ActionComponent actionComponent, ActionConfig config, ETCancellationToken cancellationToken)
        {
            Log.Info("Start FireBallCastAction");

            Cast cast = actionComponent.GetParent<Cast>();
            // 获取玩家的CastComponent
            CastComponent castComponent = cast.GetParent<CastComponent>();
            // 循环范围检测, 检测到敌人就Break
            /*while (true)
            {
                var units = self.GetBeSeePlayers().Values;
                Log.Warning("GetBeSeePlayers: " + (units.Count));
                foreach (var aoi in units)
                {
                    Unit unit = aoi.GetParent<Unit>();
                    // 这里自己也可以被火球打到
                    // 判断标签 判断Id等

                    // 判断距离
                    if (math.length(self.Position - unit.Position) < float.Parse(config.Args[0]))
                    {
                        Log.Info("FireBall 判断到敌人");

                        // 对目标数值组件-生命值伤害
                        NumericComponent numericComponent = unit.GetComponent<NumericComponent>();
                        numericComponent.Set(NumericType.Hp,
                            // 这里正常是获取数值组件.伤害 * 伤害系数
                            numericComponent.GetAsInt(NumericType.Hp) - int.Parse(config.Args[1]));
                        // 对目标添加灼烧Buff
                        BuffComponent buffComponent = unit.GetComponent<BuffComponent>();
                        buffComponent.Creat(int.Parse(config.Args[2]));
                        // 移除Cast
                        castComponent.Remove(int.Parse(config.Args[3]));
                        Log.Info("End FireBallCastAction");
                        return;
                    }
                    Log.Warning("Once Update End");
                }

                // 这里应该写移动逻辑, 但就当是个不会动的吧
            }*/

            #region 测试用 以后删掉
            // 判断命中
            // false
            // 这里开个定时器把下面逻辑放里
            // 检测到人时把定时器取消
            
            // true
            M2C_HpChanged hpChanged = new()
            {
                CasterId = cast.Owner.Id, 
            };
            foreach (long targetId in cast.Targets)
            {
                // 不同的人护甲, 减伤不一样, 所以分别计算

                Unit target = Root.Instance.Scene.GetComponent<UnitComponent>().Get(targetId);

                hpChanged.TargetId = targetId;
                hpChanged.Damage = 0; // 这里是要计算的
                // 这里用广播希望周围人也能看见自己的扣血效果
                NoticeClientHelper.Send(target, hpChanged, NoticeClientType.Broadcast);
            }
            
            // 移除Cast
            // 发送消息给客户端
            castComponent.Remove(int.Parse(config.Args[3]));

            // 对目标添加灼烧Buff
            // 发送消息给客户端
            BuffComponent buffComponent = cast.Owner.GetComponent<BuffComponent>();
            Buff buff = buffComponent.Creat(int.Parse(config.Args[2]));
            #endregion
            
            Log.Info("End FireBallCastAction");

            await ETTask.CompletedTask;
        }
    }
}