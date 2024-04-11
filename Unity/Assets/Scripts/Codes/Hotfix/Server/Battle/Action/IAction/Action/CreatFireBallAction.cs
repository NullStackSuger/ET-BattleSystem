using System.Collections.Generic;
using ET.Server;

namespace ET.Server
{

    [Action(2000)]
    [FriendOf(typeof(Cast))]
    public class CreatFireBallAction : IAction
    {
        public bool Check(ActionComponent actionComponent, ActionConfig config)
        {
            return true;
        }

        public async ETTask Run(ActionComponent actionComponent, ActionConfig config, ETCancellationToken cancellationToken)
        {
            Log.Info("Start CreatFireBallAction");

            // 获取CastComponent
            Unit unit = actionComponent.GetParent<Unit>();
            CastComponent castComponent = unit.GetComponent<CastComponent>();
            // 创建火球实体
            // 向客户端发送生成技能消息
            CastConfig castConfig = CastConfigCategory.Instance.Get(int.Parse(config.Args[0]));
            Cast cast = castComponent.Creat(castConfig.Id);
            // 移除创建火球行为
            actionComponent.Remove(2000);

            Log.Info("End CreatFireBallAction");

            await ETTask.CompletedTask;
        }
    }
}