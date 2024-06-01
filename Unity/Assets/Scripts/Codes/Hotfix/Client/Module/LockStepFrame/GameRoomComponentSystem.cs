using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [FriendOf(typeof(GameRoomComponent))]
    [FriendOf(typeof(LSFComponent))]
    public static class GameRoomComponentSystem
    {
        [FriendOf(typeof(LSFComponent))]
        public class UpdateSystem : UpdateSystem<GameRoomComponent>
        {
            protected override void Update(GameRoomComponent self)
            {
                if (self.IsStart == false) return;
                if (self.MainPlayer == null || self.MainPlayer.IsDisposed) return;

                Log.Warning(self.Frame);
                
                self.Receive();
                self.Tick();
                self.Send();
                self.NetErrorHandler();

                ++self.Frame;
            }
        }

        /// <returns>是否预测成功</returns>
        private static bool Receive(this GameRoomComponent self)
        {
            UnitComponent unitComponent = Root.Instance.Scene.GetComponent<UnitComponent>();
            LSFComponent lsf = self.MainPlayer.GetComponent<LSFComponent>();
            Queue<LSFCmd> unCheckCmds = new();
            if (!lsf.Receives.TryGetValue(self.Frame, out Queue<LSFCmd> receives)) return true;
            
            foreach (LSFCmd cmd in receives)
            {
                // 本地玩家
                if (cmd.UnitId == self.MainPlayer.Id)
                {
                    // 检查一致性
                    // 如果预测成功, 说明组件值都正确, 就不需要执行Handler了
                    // 如果预测失败, 需要回滚 Handler 追帧
                    foreach (var pair in self.MainPlayer.Components)
                    {
                        if (!LSFComponentHandlerDispatcher.Client.TryGetValue(pair.Key, out var handler)) continue;   
                        
                        if (!handler.Check(self, pair.Value, cmd))
                        {
                            unCheckCmds.Enqueue(cmd);
                        }
                    }
                }
                // 非本地玩家
                else
                {
                    LSFCmdHandlerDispatcher.Client[cmd.GetType()]?.Receive(unitComponent.Get(cmd.UnitId), cmd);
                }
            }

            if (unCheckCmds.Count > 0)
            {
                uint frame = self.Frame;
                    
                self.RollBack();
                    
                // Handler
                foreach (LSFCmd cmd in unCheckCmds)
                {
                    LSFCmdHandlerDispatcher.Client[cmd.GetType()]?.Receive(unitComponent.Get(cmd.UnitId), cmd);   
                }
                    
                // 循环Tick()
                uint count = frame - self.Frame;
                while (count-- > 0)
                {
                    self.Tick(false);
                    ++self.Frame;
                }
            }
            
            lsf.Receives.Remove(self.Frame);
            return unCheckCmds.Count <= 0;
        }

        private static void Send(this GameRoomComponent self)
        { 
            C2M_FrameCmd c2MFrameCmd = new();
            LSFComponent lsf = self.MainPlayer.GetComponent<LSFComponent>();
            if (!lsf.Sends.TryGetValue(self.Frame, out Queue<LSFCmd> sends)) return;

            foreach (LSFCmd cmd in sends)
            {
                c2MFrameCmd.Cmd = cmd;
                self.ClientScene().GetComponent<SessionComponent>().Session.Send(c2MFrameCmd);
            }

            lsf.Sends.Remove(self.Frame);
        }

        private static void RollBack(this GameRoomComponent self)
        {
            LSFComponent lsf = self.MainPlayer.GetComponent<LSFComponent>();

            foreach (LSFCmd cmd in lsf.Receives[self.Frame])
            {
                foreach (var pair in self.MainPlayer.Components)
                {
                    if (!LSFComponentHandlerDispatcher.Client.TryGetValue(pair.Key, out var handler)) continue;
                    
                    handler.RollBack(self, pair.Value, cmd);
                }   
            }
        }

        /// <summary>
        /// 这个Tick控制帧率相关
        /// </summary>
        private static void Tick(this GameRoomComponent self, bool needSend = true)
        {
            List<(LSFComponentHandler, Entity)> handlers = new();
            Unit unit = self.MainPlayer;
            foreach (var pair in unit.Components)
            {
                if (!LSFComponentHandlerDispatcher.Client.TryGetValue(pair.Key, out var handler)) continue;

                handlers.Add((handler, pair.Value));
            }
            
            foreach (var handler in handlers)
            {
                handler.Item1.TickStart(self, handler.Item2, needSend);
            }
            foreach (var handler in handlers)
            {
                handler.Item1.Tick(self, handler.Item2, needSend);
            }
            foreach (var handler in handlers)
            {
                handler.Item1.TickEnd(self, handler.Item2, needSend);
            }
        }

        /// <summary>
        /// 处理网络异常问题
        /// 主要处理客户端服务端帧数
        /// </summary>
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
            }
        } 
    }
}