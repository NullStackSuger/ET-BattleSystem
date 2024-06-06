
namespace ET.Client
{
    [FriendOf(typeof(GameRoomComponent))]
    public abstract class LSFHandler<T, K> : ILSFHandler where T : Entity where K : LSFCmd
    {
        public abstract void TickStart(GameRoomComponent room, T component, bool inRollBack);
        public abstract void Tick(GameRoomComponent room, T component, bool inRollBack);
        public abstract LSFCmd TickEnd(GameRoomComponent room, T component, bool inRollBack);

        public abstract void Receive(Unit unit, T component, K cmd);
        public abstract bool Check(K clientCmd, K serverCmd);
        public abstract void RollBack(GameRoomComponent room, T component, K cmd);

        public void OnTickStart(GameRoomComponent room, Entity component, bool inRollBack)
        {
            if (room == null || component == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}");
                return;
            }

            TickStart(room, component as T, inRollBack);
        }
        public void OnTick(GameRoomComponent room, Entity component, bool inRollBack)
        {
            if (room == null || component == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}");
                return;
            }

            Tick(room, component as T, inRollBack);
        }
        public void OnTickEnd(GameRoomComponent room, Entity component, bool inRollBack)
        {
            if (room == null || component == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}");
                return;
            }

            LSFCmd cmd = TickEnd(room, component as T, inRollBack);
            if (cmd != null) room.AllCmds[cmd.Frame, cmd.GetType()] = cmd;
            // TODO: 这里最好是根据inRollBack判断是否发送消息, 但这里是Model层无法调用AddToSend
        }

        public void OnReceive(GameRoomComponent room, LSFCmd cmd)
        {
            if (room == null || cmd == null)
            {
                Log.Error($"参数为空: Room {room == null}, Cmd {cmd == null}");
                return;
            }

            UnitComponent unitComponent = room.DomainScene().GetComponent<CurrentScenesComponent>().Scene.GetComponent<UnitComponent>();
            Unit unit = cmd.UnitId == -1 ?
                    room.MainPlayer :
                    unitComponent.GetChild<Unit>(cmd.UnitId);
            if (unit == null)
            {
                Log.Error($"未找到{cmd.UnitId}Unit");
                return;
            }

            T component = unit.GetComponent<T>();
            if (component == null)
            {
                Log.Error($"未找到{typeof(T)}组件");
                return;
            }

            Receive(unit, component, cmd as K);
        }
        public bool OnCheck(LSFCmd clientCmd, LSFCmd serverCmd)
        {
            return Check(clientCmd as K, serverCmd as K);
        }
        public void OnRollBack(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            if (room == null || component == null || cmd == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}, Cmd {cmd == null}");
                return;
            }

            RollBack(room, component as T, cmd as K);
        }
    }

    public interface ILSFHandler
    {
        public void OnTickStart(GameRoomComponent room, Entity component, bool inRollBack);
        public void OnTick(GameRoomComponent room, Entity component, bool inRollBack);
        public void OnTickEnd(GameRoomComponent room, Entity component, bool inRollBack);
        
        public void OnReceive(GameRoomComponent room, LSFCmd cmd);
        public bool OnCheck(LSFCmd clientCmd, LSFCmd serverCmd);
        public void OnRollBack(GameRoomComponent room, Entity component, LSFCmd cmd);
    }
}