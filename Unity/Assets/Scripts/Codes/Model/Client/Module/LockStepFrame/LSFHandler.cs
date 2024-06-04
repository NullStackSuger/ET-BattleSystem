namespace ET.Client
{
    public abstract class LSFHandler<T, K> : ILSFHandler where T : Entity where K : LSFCmd
    {
        public abstract void TickStart(GameRoomComponent room, T component, bool inRollBack);
        public abstract void Tick(GameRoomComponent room, T component, bool inRollBack);
        public abstract void TickEnd(GameRoomComponent room, T component, bool inRollBack);
        
        public abstract void Receive(Unit unit, K cmd);
        public abstract bool Check(GameRoomComponent room, T component, K cmd);
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
            
            TickEnd(room, component as T, inRollBack);
        }

        //TODO: 问题 : unitComponent.Get(cmd.UnitId) == null
        // 如何获取服务端对应客户端的Unit
        public void OnReceive(GameRoomComponent room, LSFCmd cmd)
        {
            if (room == null || cmd == null)
            {
                Log.Error($"参数为空: Room {room == null}, Cmd {cmd == null}");
                return;
            }

            Unit unit = null;
            if (cmd.UnitId == -1)
                unit = room.MainPlayer;
            else
                unit = room.DomainScene().GetComponent<UnitComponent>().GetChild<Unit>(cmd.UnitId);
            if (unit == null)
            {
                Log.Error($"未找到{cmd.UnitId}Unit");
                return;
            }
            
            Receive(unit, cmd as K);
        }
        public bool OnCheck(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            if (room == null || component == null || cmd == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}, Cmd {cmd == null}");
                return true;
            }

            Unit unit = null;
            if (cmd.UnitId == -1)
                unit = room.MainPlayer;
            else
                unit = room.DomainScene().GetComponent<UnitComponent>().GetChild<Unit>(cmd.UnitId);
            if (unit == null)
            {
                Log.Error($"未找到{cmd.UnitId}Unit");
                return true;
            }
            
            return Check(room, component as T, cmd as K);
        }
        public void OnRollBack(GameRoomComponent room, Entity component, LSFCmd cmd)
        {
            if (room == null || component == null || cmd == null)
            {
                Log.Error($"参数为空: Room {room == null}, Component {component == null}, Cmd {cmd == null}");
                return;
            }

            Unit unit = null;
            if (cmd.UnitId == -1)
                unit = room.MainPlayer;
            else
                unit = room.DomainScene().GetComponent<UnitComponent>().GetChild<Unit>(cmd.UnitId);
            if (unit == null)
            {
                Log.Error($"未找到{cmd.UnitId}Unit");
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
        public bool OnCheck(GameRoomComponent room, Entity component, LSFCmd cmd);
        public void OnRollBack(GameRoomComponent room, Entity component, LSFCmd cmd);
    }
}