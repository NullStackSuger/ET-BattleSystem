namespace ET.Server
{

    [Action(1000)]
    [FriendOf(typeof(Buff))]
    public class BurnBuffAction : IAction
    {
        public bool Check(ActionComponent actionComponent, ActionConfig config)
        {
            return true;
        }

        public async ETTask Run(ActionComponent actionComponent, ActionConfig config, ETCancellationToken cancellationToken)
        {
            Log.Info("Start BurnBuffAction");

            // 获取NumericComponent
            //NumericComponent numericComponent = actionComponent.GetParent<Unit>().GetComponent<NumericComponent>();
            // 获取BuffComponent
            Buff buff = actionComponent.GetParent<Buff>();
            BuffComponent buffComponent = buff.GetParent<BuffComponent>();
            // 循环直到Buff时间结束 扣血
            //long endTime = TimeHelper.ClientNow() + int.Parse(config.Args[0]);
            //while (TimeHelper.ClientNow() < endTime)
            //{
            //Log.Info("BurnBuff 持续扣血");

            //numericComponent.Set(NumericType.Hp, numericComponent.GetAsInt(NumericType.Hp) - int.Parse(config.Args[1]));
            
            // 同时还应该有M2C_HpChanged 发送给客户端
            //}

            // 移除Buff(因为行为在Buff下, 同时行为也被移除)
            buffComponent.Remove(int.Parse(config.Args[2]));
            M2C_BuffRemove buffRemove = new()
            {
                BuffId = buff.Id, 
                CasterId = buff.Owner.Id, 
                TargetsId = new(buff.Targets),
            };
            NoticeClientHelper.Send(buff.Owner, buffRemove,
                (NoticeClientType)BuffConfigCategory.Instance.Get(buff.ConfigId).NoticeClientType);

            Log.Info("End BurnBuffAction");
            await ETTask.CompletedTask;
        }
    }
}