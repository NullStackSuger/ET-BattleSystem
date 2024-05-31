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
                case NoticeClientType.Broad:
                    foreach (var aoi in self.GetBeSeePlayers().Values)
                    {
                        if (aoi.Unit == null || aoi.Unit.IsDisposed) continue;
                        Send(self, message, NoticeClientType.Self);
                    }
                    break;
                case NoticeClientType.BroadWithoutSelf:
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
        
        public static void Send(Unit self, IActorLocationMessage message, NoticeClientType type)
        {
            switch (type)
            {
                case NoticeClientType.Self:
                    UnitGateComponent unitGateComponent = self.GetComponent<UnitGateComponent>();
                    if (unitGateComponent == null) return;
                    if (unitGateComponent.GateSessionActorId == 0) return;
                    MessageHelper.SendToLocationActor(unitGateComponent.GateSessionActorId, message);
                    break;
                case NoticeClientType.Broad:
                    foreach (var aoi in self.GetBeSeePlayers().Values)
                    {
                        if (aoi.Unit == null || aoi.Unit.IsDisposed) continue;
                        Send(self, message, NoticeClientType.Self);
                    }
                    break;
                case NoticeClientType.BroadWithoutSelf:
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
        
        public static async ETTask Call(Unit self, IActorLocationRequest message, NoticeClientType type)
        {
            switch (type)
            {
                case NoticeClientType.Self:
                    UnitGateComponent unitGateComponent = self.GetComponent<UnitGateComponent>();
                    if (unitGateComponent == null) return;
                    if (unitGateComponent.GateSessionActorId == 0) return;
                    await MessageHelper.CallLocationActor(unitGateComponent.GateSessionActorId, message);
                    break;
                case NoticeClientType.Broad:
                    foreach (var aoi in self.GetBeSeePlayers().Values)
                    {
                        if (aoi.Unit == null || aoi.Unit.IsDisposed) continue;
                        await Call(self, message, NoticeClientType.Self);
                    }
                    break;
                case NoticeClientType.BroadWithoutSelf:
                    foreach (var aoi in self.GetBeSeePlayers().Values)
                    {
                        if (aoi.Unit == null || aoi.Unit.IsDisposed) continue;
                        if (aoi.Unit.Id == self.Id) continue;
                        await Call(self, message, NoticeClientType.Self);
                    }
                    break;
                case NoticeClientType.None:
                    break;
            }
        }
    }
}