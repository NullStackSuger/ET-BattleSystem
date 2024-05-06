using System.Collections.Generic;

namespace ET.Server
{

    [Action(0001)]
    [FriendOfAttribute(typeof(ET.Cast))]
    public class NullCastAction : IAction
    {
        public bool Check(ActionComponent actionComponent, ActionConfig config)
        {
            return true;
        }

        public async ETTask Run(ActionComponent actionComponent, ActionConfig config, ETCancellationToken cancellationToken)
        {
            Log.Info("TestCastAction Run");
            Cast cast = actionComponent.GetParent<Cast>();
            NoticeClientHelper.Send(cast.Owner, new M2C_CastCreat()
            {
                CastId = cast.Id, 
                CasterId = cast.Owner.Id, 
                TargetsId = new List<long>(cast.Targets)
            }, NoticeClientType.Self);
            await ETTask.CompletedTask;
        }
    }
}