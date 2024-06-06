using System.Collections.Generic;
using System.Linq;

namespace ET.Server
{
    [FriendOf(typeof(GameRoomComponent))]
    public static class GameRoomComponentSystem
    {
        public class AwakeSystem : AwakeSystem<GameRoomComponent>
        {
            protected override void Awake(GameRoomComponent self)
            {
                Game.Ticker.UpdateCallback += self.TickOuter;
            }
        }
        
        public class UpdateSystem : UpdateSystem<GameRoomComponent>
        {
            protected override void Update(GameRoomComponent self)
            {
                TickOuter(self);
            }
        }

        public class DestroySystem : DestroySystem<GameRoomComponent>
        {
            protected override void Destroy(GameRoomComponent self)
            {
                Game.Ticker.UpdateCallback -= self.TickOuter;
                self.Syncs?.Clear();
                self.Syncs = null;
            }
        }

        private static void TickOuter(this GameRoomComponent self)
        {
            if (!self.IsStart) return;
            if (self.Syncs.Count <= 0) return;
            
            self.Receive();
            self.Tick();
            self.Send();
                
            ++self.Frame;
        }

        private static void Receive(this GameRoomComponent self)
        {
            if (self.Receives.Count <= 0) return;
            
            SortedSet<LSFCmd> receives = self.Receives.First().Value;

            foreach (LSFCmd cmd in receives)
            {
                LSFHandlerDispatcher.Handlers[cmd.GetType()]?.OnReceive(self, cmd);
            }

            self.Receives.Remove(receives.First().Frame);
        }

        private static void Send(this GameRoomComponent self)
        {
            if (self.Sends.Count <= 0) return;
            
            SortedSet<LSFCmd> sends = self.Sends.First().Value;
            
            M2C_FrameCmd m2CFrameCmd = new();
            UnitComponent unitComponent = self.DomainScene().GetComponent<UnitComponent>();

            foreach (LSFCmd cmd in sends)
            {
                Unit unit = unitComponent.Get(cmd.UnitId);
                m2CFrameCmd.Cmd = cmd;
                NoticeClientHelper.Send(unit, m2CFrameCmd, NoticeClientType.Broad);
            }

            self.Sends.Remove(sends.First().Frame);
        }
        
        private static void Tick(this GameRoomComponent self)
        {
            // 检查Syncs是否存在
            for (int i = self.Syncs.Count - 1; i >= 0; --i)
            {
                Unit unit = self.Syncs[i];
                if (unit == null || unit.IsDisposed)
                {
                    self.Syncs.RemoveAt(i);
                    continue;
                }
            }
            
            // 记录所有Syncs的Handler
            List<(ILSFHandler, Entity)> handlers = new();
            foreach (Unit unit in self.Syncs)
            {
                foreach (var pair in unit.Components)
                {
                    if (!LSFHandlerDispatcher.Handlers.TryGetValue(pair.Key, out var handler)) continue;

                    handlers.Add((handler, pair.Value));
                }
            }
            
            foreach (var pair in handlers)
            {
                var handler = pair.Item1;
                var component = pair.Item2;
                
                handler.OnTickStart(self, component);
            }
            foreach (var pair in handlers)
            {
                var handler = pair.Item1;
                var component = pair.Item2;
                
                handler.OnTick(self, component);
            }
            foreach (var pair in handlers)
            {
                var handler = pair.Item1;
                var component = pair.Item2;
                
                handler.OnTickEnd(self, component);
            }
        }
        
        public static void AddToSend(this GameRoomComponent self, LSFCmd cmd)
        {
            if (!self.Sends.ContainsKey(cmd.Frame))
                self.Sends.Add(cmd.Frame, new SortedSet<LSFCmd>());
            self.Sends[cmd.Frame].Add(cmd);
            
            // 需要发送的消息都是服务器验证过的
            // 所以在发送时记录而不是接收时
            self.RecordCmd(cmd);
        }

        public static void AddToReceive(this GameRoomComponent self, LSFCmd cmd)
        {
            if (!self.Receives.ContainsKey(cmd.Frame))
                self.Receives.Add(cmd.Frame, new SortedSet<LSFCmd>());
            self.Receives[cmd.Frame].Add(cmd);
        }

        /// <summary>
        /// 添加一个被帧同步的Unit
        /// </summary>
        public static void TryAddSync(this GameRoomComponent self, Unit unit)
        {
            if (self.Syncs.Contains(unit)) return;
            self.Syncs.Add(unit);
        }

        /// <summary>
        /// 记录Cmd
        /// </summary>
        public static void RecordCmd(this GameRoomComponent self, LSFCmd cmd)
        {
            if (!self.AllCmds.ContainsKey(cmd.Frame))
                self.AllCmds.Add(cmd.Frame, new SortedSet<LSFCmd>());
            self.AllCmds[cmd.Frame].Add(cmd);
        }
    }
}