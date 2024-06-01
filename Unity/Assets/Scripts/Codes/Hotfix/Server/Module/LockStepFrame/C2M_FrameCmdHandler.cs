namespace ET.Server
{
    [ActorMessageHandler(SceneType.Map)]
    [FriendOfAttribute(typeof(ET.Server.GameRoomComponent))]
    public class C2M_FrameCmdHandler : AMActorLocationHandler<Unit, C2M_FrameCmd>
    {
        protected override async ETTask Run(Unit entity, C2M_FrameCmd message)
        {
            Log.Warning("C2M_FrameCmd");
            GameRoomComponent room = Root.Instance.Scene.GetComponent<GameRoomComponent>();
            room.TryAddSync(entity);
            LSFComponent lsf = entity.GetComponent<LSFComponent>();
            lsf.AddToReceive(message.Cmd);
            room.IsStart = true;
            await ETTask.CompletedTask;
        }
    }
}