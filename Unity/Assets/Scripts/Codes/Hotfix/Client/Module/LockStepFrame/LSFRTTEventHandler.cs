/*using ET.EventType;

namespace ET.Client
{
    /// <summary>
    /// 用于定时通信服务端计算TargetRTT
    /// </summary>
    [Event(SceneType.Client)]
    public class LSFRTTEventHandler : AEvent<EventType.AppStartInitFinish>
    {
        protected override async ETTask Run(Scene scene, AppStartInitFinish a)
        {
            ETCancellationToken cancellationToken = new();
            //CallServer(Root.Instance.Scene.GetComponent<GameRoomComponent>(), cancellationToken, 10).Coroutine();
            await ETTask.CompletedTask;
        }

        private async ETTask CallServer(GameRoomComponent clientRoom, ETCancellationToken cancellationToken, float waitTime)
        {
            while (!cancellationToken.IsCancel())
            {
                await TimerComponent.Instance.WaitAsync(10);
                long beforeTime = TimeHelper.ClientNow();
                M2C_Ping m2CPing = await clientRoom.DomainScene().GetComponent<SessionComponent>().Session.Call(new C2M_Ping()) as M2C_Ping;
                long afterTime = TimeHelper.ClientNow();
                long frame = TimeHelper.ToFrame(afterTime - beforeTime, clientRoom.TickRateFrame);
                clientRoom.TargetAhead = (uint)(frame % 2 == 0? frame / 2 : frame / 2 + 1);
            }
        }
    }
}*/