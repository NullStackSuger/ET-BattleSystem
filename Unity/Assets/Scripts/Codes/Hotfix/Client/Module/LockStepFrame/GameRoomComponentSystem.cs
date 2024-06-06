using System;
using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    // TODO: 考虑下退出流程, 现在因为停止运行, 导致await报错
    // TODO: 但是之前在Update里还正常, 这里定时执行才有问题 可能是TCP粘包导致大部分包一次性被发送 导致消息没有被即使处理, CurrentAhead计算出问题
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
            }
        }

        /// <summary>
        /// 每次Update需要走的流程, 放函数里方便更新频率时使用
        /// </summary>
        private static void TickOuter(this GameRoomComponent self)
        {
            if (self.MainPlayer == null || self.MainPlayer.IsDisposed) return;
                
            self.NetErrorHandler();
            self.Receive();
            self.Tick();
            self.Send();
                
            ++self.Frame; 
        }

        /// <returns>是否预测成功</returns>
        private static void Receive(this GameRoomComponent self)
        {
            if (self.Receives.Count <= 0) return;

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

                    // 客户端没有历史记录, 就判断通过
                    if (!LSFHandlerDispatcher.Handler.TryGetValue(cmd.GetType(), out var handler)) continue;
                    if (!self.AllCmds.TryGetValue(cmd.Frame, cmd.GetType(), out LSFCmd clientCmd)) continue;

                    // 客户端服务端Cmd不相同
                    if (!handler.OnCheck(clientCmd, cmd))
                    {
                        Log.Warning($"Cmd未通过一致检测 {cmd.GetType()}");
                        unCheckCmds.Enqueue(cmd);
                    }
                }
                // 非本地玩家
                else
                {
                    LSFHandlerDispatcher.Handler[cmd.GetType()]?.OnReceive(self, cmd);
                }
            }

            if (unCheckCmds.Count > 0)
            {
                uint frame = self.Frame;
                self.Frame = unCheckCmds.First().Frame;

                foreach (LSFCmd cmd in unCheckCmds)
                {
                    self.RollBack(cmd);
                    LSFHandlerDispatcher.Handler[cmd.GetType()]?.OnReceive(self, cmd);
                }

                // 循环Tick()
                uint count = frame - self.Frame;
                while (count-- > 0)
                {
                    self.Tick(true);
                    ++self.Frame;
                }
            }

            self.Receives.Remove(receives.First().Frame);
        }

        private static void Send(this GameRoomComponent self)
        { 
            if (self.Sends.Count <= 0) return;
            
            SortedSet<LSFCmd> sends = self.Sends.First().Value;

            C2M_FrameCmdReq c2MFrameCmd = new();
            
            foreach (LSFCmd cmd in sends)
            {
                c2MFrameCmd.Cmd = cmd;
                ClientSceneManagerComponent.Instance.Get(1).GetComponent<SessionComponent>().Session.Call(c2MFrameCmd).Coroutine();
            }

            self.Sends.Remove(sends.First().Frame);
        }

        private static void RollBack(this GameRoomComponent self, LSFCmd cmd)
        {
            foreach (var pair in self.MainPlayer.Components)
            {
                if (!LSFHandlerDispatcher.Handler.TryGetValue(pair.Key, out var handler)) continue;
                    
                handler.OnRollBack(self, pair.Value, cmd);
            }   
        }
        
        private static void Tick(this GameRoomComponent self, bool inRollBack = false)
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
            // 赋值LastReceiveFrame
            if (self.Receives.Count > 0)
            {
                self.LastReceiveFrame = self.Receives.Last().Key;
            }

            float currentAhead = (self.Frame - self.LastReceiveFrame) * 0.5f;
            
            // 客户端帧数 < 服务端
            // 因为开局的时候由于网络延迟问题导致服务端先行于客户端，直接多次tick
            if (currentAhead < 0)
            {
                // 多次Tick, 追到领先服务端HalfRTT + Buffer
                for (int i = 0; i < -currentAhead + self.TargetAhead + 0; ++i)
                {
                    self.Tick();
                    ++self.Frame;
                }
            }
            
            // 客户端领先帧数过大, 判定为掉线
            if (currentAhead > GameRoomComponent.MaxAhead)
            {
                // 等待3s
                Log.Warning("掉线");
            }
            
            // 如果进入上面if Tick数值会被改变, 要重新计算
            currentAhead = (self.Frame - self.LastReceiveFrame) * 0.5f;
            //self.Ticker.TargetElapsedTime = TimeSpan.FromTicks(TimeSpan.TicksPerSecond / (long)(30 + self.TargetAhead - currentAhead));
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