namespace ET.Server
{

    public abstract class LSFHandler<T, K> : ILSFHandler where T : Entity where K : LSFCmd
    {
        public abstract void TickStart(GameRoomComponent room, T component);
        public abstract void Tick(GameRoomComponent room, T component);
        public abstract void TickEnd(GameRoomComponent room, T component);
        
        public abstract void Receive(Unit unit, T component, K cmd);
        
        public void OnTickStart(GameRoomComponent room, Entity component)
        {
            if (room == null || component == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}");
                return;
            }
            
            TickStart(room, component as T);
        }
        public void OnTick(GameRoomComponent room, Entity component)
        {
            if (room == null || component == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}");
                return;
            }
            
            Tick(room, component as T);
        }
        public void OnTickEnd(GameRoomComponent room, Entity component)
        {
            if (room == null || component == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}");
                return;
            }
            
            TickEnd(room, component as T);
        }
        
        public void OnReceive(GameRoomComponent room, LSFCmd cmd)
        {
            if (room == null || cmd == null)
            {
                Log.Error($"参数为空: Room {room == null}, Cmd {cmd == null}");
                return;
            }

            Unit unit = room.DomainScene().GetComponent<UnitComponent>().GetChild<Unit>(cmd.UnitId);
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
    }

    public interface ILSFHandler
    {
        public void OnTickStart(GameRoomComponent room, Entity component);
        public void OnTick(GameRoomComponent room, Entity component);
        public void OnTickEnd(GameRoomComponent room, Entity component);
        
        public void OnReceive(GameRoomComponent room, LSFCmd cmd);
    }
}