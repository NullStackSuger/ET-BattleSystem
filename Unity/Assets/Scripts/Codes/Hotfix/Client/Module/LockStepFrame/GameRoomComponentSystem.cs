using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [FriendOf(typeof(GameRoomComponent))]
    public static class GameRoomComponentSystem
    {
        public class UpdateSystem : UpdateSystem<GameRoomComponent>
        {
            protected override void Update(GameRoomComponent self)
            {
                if (self.MainPlayer == null || self.MainPlayer.IsDisposed) return;
                
                //self.NetErrorHandler();
                self.Receive();
                self.Tick();
                self.Send();
                
                ++self.Frame;
            }
        }

        /// <returns>是否预测成功</returns>
        private static bool Receive(this GameRoomComponent self)
        {
            if (self.Receives.Count <= 0) return true;
            
            SortedSet<LSFCmd> receives = self.Receives.First().Value;
            Queue<LSFCmd> unCheckCmds = new();
            
            foreach (LSFCmd cmd in receives)
            {
                // 本地玩家
                if (cmd.UnitId == self.MainPlayer.Id)
                {
                    // 检查一致性
                    // 如果预测成功, 说明组件值都正确, 就不需要执行Handler了
                    // 如果预测失败, 需要回滚 Handler 追帧
                    if (!LSFHandlerDispatcher.Handler.TryGetValue(cmd.GetType(), out var handler)) continue;

                    Entity component = self.MainPlayer.GetComponent(LSFHandlerDispatcher.Types[cmd.GetType()]);
                        
                    if (!handler.OnCheck(self, component, cmd))
                    {
                        unCheckCmds.Enqueue(cmd);
                    }
                    else
                    {

                    }
                }
                // 非本地玩家
                else
                {
                    Log.Warning("非本地玩家");
                    LSFHandlerDispatcher.Handler[cmd.GetType()]?.OnReceive(self, cmd);
                }
            }

            if (unCheckCmds.Count > 0)
            {
                uint frame = self.Frame;
                
                foreach (LSFCmd cmd in unCheckCmds)
                {
                    self.RollBack(cmd);
                    LSFHandlerDispatcher.Handler[cmd.GetType()]?.OnReceive(self, cmd);   
                }
                    
                // 循环Tick()
                uint count = frame - self.Frame;
                while (count-- > 0)
                {
                    self.Tick(false);
                    ++self.Frame;
                }
            }
            
            self.Receives.Remove(receives.First().Frame);
            return unCheckCmds.Count <= 0;
        }

        private static void Send(this GameRoomComponent self)
        { 
            if (!self.Sends.TryGetValue(self.Frame, out SortedSet<LSFCmd> sends)) return;

            C2M_FrameCmdReq c2MFrameCmd = new();
            
            foreach (LSFCmd cmd in sends)
            {
                c2MFrameCmd.Cmd = cmd;
                ClientSceneManagerComponent.Instance.Get(1).GetComponent<SessionComponent>().Session.Call(c2MFrameCmd).Coroutine();
            }

            self.Sends.Remove(self.Frame);
        }

        private static void RollBack(this GameRoomComponent self, LSFCmd cmd)
        {
            foreach (var pair in self.MainPlayer.Components)
            {
                if (!LSFHandlerDispatcher.Handler.TryGetValue(pair.Key, out var handler)) continue;
                    
                handler.OnRollBack(self, pair.Value, cmd);
            }   
        }
        
        private static void Tick(this GameRoomComponent self, bool inRollBack = true)
        {
            Unit unit = self.MainPlayer;
            List<(ILSFHandler, Entity)> handlers = new();
            foreach (var pair in unit.Components)
            {
                if (!LSFHandlerDispatcher.Handler.TryGetValue(pair.Key, out var handler)) continue;
                
                handlers.Add((handler, pair.Value));
            }
            
            foreach (var pair in handlers)
            {
                var handler = pair.Item1;
                var component = pair.Item2;
                
                handler.OnTickStart(self, component, inRollBack);
            }
            foreach (var pair in handlers)
            {
                var handler = pair.Item1;
                var component = pair.Item2;
                
                handler.OnTick(self, component, inRollBack);
            }
            foreach (var pair in handlers)
            {
                var handler = pair.Item1;
                var component = pair.Item2;
                
                handler.OnTickEnd(self, component, inRollBack);
            }
        }
        
        private static void NetErrorHandler(this GameRoomComponent self)
        {
            // 客户端帧数 < 服务端
            // 因为开局的时候由于网络延迟问题导致服务端先行于客户端，直接多次tick(追帧)
            if (self.CurrentAhead < 0)
            {
                // 多次Tick, 追到领先服务端HalfRTT + Buffer
                for (int i = 0; i < -self.CurrentAhead * 2 + 0; ++i)
                {
                    self.Tick();
                    ++self.Frame;
                }
            }
            
            // 客户端领先帧数过大, 判定为掉线
            if (self.CurrentAhead > GameRoomComponent.MaxAhead)
            {
                // 等待3s
                Log.Warning("掉线");
            }
        }

        public static void AddToSend(this GameRoomComponent self, LSFCmd cmd)
        {
            if (!self.Sends.ContainsKey(cmd.Frame))
                self.Sends.Add(cmd.Frame, new SortedSet<LSFCmd>());
            self.Sends[cmd.Frame].Add(cmd);
        }

        public static void AddToReceive(this GameRoomComponent self, LSFCmd cmd)
        {
            if (!self.Receives.ContainsKey(cmd.Frame))
                self.Receives.Add(cmd.Frame, new SortedSet<LSFCmd>());
            self.Receives[cmd.Frame].Add(cmd);
        }
    }
}