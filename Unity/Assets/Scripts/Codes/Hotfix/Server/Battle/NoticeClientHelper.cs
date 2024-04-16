namespace ET.Server
{
    
    public static class NoticeClientHelper
    {
        public static void Send(Unit self, IActorMessage message, NoticeClientType type)
        {
            switch (type)
            {
                case NoticeClientType.Self:
                    UnitGateComponent unitGateComponent = self.GetComponent<UnitGateComponent>();
                    if (unitGateComponent == null) return;
                    if (unitGateComponent.GateSessionActorId == 0) return;
                    MessageHelper.SendActor(unitGateComponent.GateSessionActorId, message);
                    break;
                case NoticeClientType.Broadcast:
                    foreach (var aoi in self.GetBeSeePlayers().Values)
                    {
                        if (aoi.Unit == null || aoi.Unit.IsDisposed) continue;
                        Send(self, message, NoticeClientType.Self);
                    }
                    break;
                case NoticeClientType.BroadcastWithoutSelf:
                    foreach (var aoi in self.GetBeSeePlayers().Values)
                    {
                        if (aoi.Unit == null || aoi.Unit.IsDisposed) continue;
                        if (aoi.Unit.Id == self.Id) continue;
                        Send(self, message, NoticeClientType.Self);
                    }
                    break;
                case NoticeClientType.None:
                    break;
            }
        }
    }
}