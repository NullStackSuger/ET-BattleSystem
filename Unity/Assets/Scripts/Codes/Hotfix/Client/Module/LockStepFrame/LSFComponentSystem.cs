using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [FriendOfAttribute(typeof(ET.Client.LSFComponent))]
    [FriendOfAttribute(typeof(ET.Client.LSFComponentHandlerDispatcherComponent))]
    [FriendOfAttribute(typeof(ET.Client.LSFCmdHandlerDispatcherComponent))]
    public static class LSFComponentSystem
    {
        public class LSFComponentAwakeSystem : AwakeSystem<LSFComponent>
        {
            protected override void Awake(LSFComponent self)
            {

            }
        }

        public class LSFComponentUpdateSystem : UpdateSystem<LSFComponent>
        {
            protected override void Update(LSFComponent self)
            {
                self.Tick();
                self.NetErrorHandler();
            }
        }

        public class LSFComponentDestroySystem : DestroySystem<LSFComponent>
        {
            protected override void Destroy(LSFComponent self)
            {

            }
        }

        /// <summary>
        /// 把cmd加入待发送队列
        /// </summary>
        public static void SendCmd(this LSFComponent self, LSFCmd cmd)
        {
            if (!self.Sends.ContainsKey(cmd.Frame))
                self.Sends.Add(cmd.Frame, new Queue<LSFCmd>());
            self.Sends[cmd.Frame].Enqueue(cmd);
        }

        /// <summary>
        /// 把cmd加入待处理队列
        /// </summary>
        public static void ReceiveCmd(this LSFComponent self, LSFCmd cmd)
        {
            if (!self.Receives.ContainsKey(cmd.Frame))
                self.Receives.Add(cmd.Frame, new Queue<LSFCmd>());
            self.Receives[cmd.Frame].Enqueue(cmd);
        }

        /// <summary>
        /// 进行一次Tick
        /// 几乎所有函数都用来辅助它运行
        /// </summary>
        private static void Tick(this LSFComponent self)
        {
            if (!self.IsOnline) return;
            
            // 获取到当前Player
            //Unit player = self.DomainScene().GetComponent<UnitComponent>()
            // TODO: 获取当前客户端控制的Unit
            Unit player = null;

            // 处理接收到的Cmd
            if (self.Receives.Count > 0)
            {
                Queue<LSFCmd> receives = self.Receives.First().Value;
                Queue<LSFCmd> unCheckCmds = new();

                foreach (LSFCmd cmd in receives)
                {
                    // 非本地玩家
                    if (cmd.UnitId != player.Id)
                    {
                        LSFCmdHandlerDispatcherComponent.Instance.Handlers[cmd.GetType()]?.Receive(cmd);
                    }
                    // 本地玩家
                    else
                    {
                        // 检查一致性
                        // 如果预测成功, 说明组件值都正确, 就不需要执行Handler了
                        // 如果预测失败, 需要回滚 Handler 追帧
                        foreach (var pair in player.Components)
                        {
                            if (!LSFComponentHandlerDispatcherComponent.Instance.Handlers.TryGetValue(pair.Key, out var handler)) continue;

                            if (!handler.Check(self, pair.Value, cmd))
                            {
                                unCheckCmds.Enqueue(cmd);
                            }
                        }
                    }
                }
                
                // 预测失败
                if (unCheckCmds.Count > 0)
                {
                    uint frame = self.CurrentFrame;
                    // 回滚
                    foreach (LSFCmd cmd in receives)
                    {
                        self.RollBack(cmd);
                    }
                    // Handler
                    foreach (LSFCmd cmd in unCheckCmds)
                    {
                        LSFCmdHandlerDispatcherComponent.Instance.Handlers[cmd.GetType()]?.Receive(cmd);   
                    }
                    // 追帧[CurrentFrame, frame)
                    uint count = frame - self.CurrentFrame;
                    while (count-- > 0)
                    {
                        self.TickFunc();
                        ++self.CurrentFrame;
                    }
                }

                self.Receives.Remove(self.CurrentFrame);
            }

            // 调用Tick
            self.TickFunc();

            // 发送cmd
            if (self.Sends.TryGetValue(self.CurrentFrame, out Queue<LSFCmd> sends))
            {
                C2M_FrameCmd c2MFrameCmd = new();
                while (sends.Count > 0)
                {
                    LSFCmd cmd = sends.Dequeue();
                    c2MFrameCmd.Cmd = cmd;

                    self.ClientScene().GetComponent<SessionComponent>().Session.Call(c2MFrameCmd).Coroutine();
                }

                self.Sends.Remove(self.CurrentFrame);
            }

            ++self.CurrentFrame;
        }

        /// <summary>
        /// 回滚到指定帧
        /// </summary>
        private static void RollBack(this LSFComponent self, LSFCmd cmd)
        {
            self.CurrentFrame = cmd.Frame;

            foreach (var pair in self.Components)
            {
                if (LSFComponentHandlerDispatcherComponent.Instance.Handlers.TryGetValue(pair.Key, out var handler)) continue;
                
                handler.RollBack(self,  pair.Value, cmd);
            }
        }
        
        /// <summary>
        /// 调用TickStart Tick TickEnd
        /// </summary>
        private static void TickFunc(this LSFComponent self)
        {
            List<(LSFComponentHandler, Entity)> handlers = new();
            foreach (var pair in self.Components)
            {
                if (!LSFComponentHandlerDispatcherComponent.Instance.Handlers.TryGetValue(pair.Key, out var handler)) continue;

                handlers.Add((handler, pair.Value));
            }

            foreach (var handler in handlers)
            {
                handler.Item1.TickStart(self, handler.Item2);
            }
            foreach (var handler in handlers)
            {
                handler.Item1.Tick(self, handler.Item2);
            }
            foreach (var handler in handlers)
            {
                handler.Item1.TickEnd(self, handler.Item2);
            }
        }

        /// <summary>
        /// 用于处理网络异常
        /// </summary>
        private static void NetErrorHandler(this LSFComponent self)
        {
            /*float aheadFrame = self.CurrentFrame - self.ServerCurrentFrame;
            // 当前客户端帧数大于服务端帧数
            // 1.正常情况，客户端为了保证自己的消息在合适的时间点抵达服务端需要领先于服务器
            // 2.非正常情况，客户端由于网络延迟或者断开导致没有收到服务端的帧指令，导致ServerCurrentFrame长时间没有更新，
            //   再次收到服务端回包的时候发现是很久之前包了，也就会导致CurrentAheadOfFrame变大，当达到一个阈值的时候将会进行断线重连
            if (aheadFrame > 0)
            {
                // 客户端掉线
                if (aheadFrame > LSFComponent.MaxAheadFrame)
                {
                    self.IsOnline = false;
                    // 模拟断线重连
                    //TODO: 等待3s
                    self.IsOnline = true;
                    
                    //???
                    return;
                }
            }
            // 当前客户端帧数小于服务端帧数
            // 因为开局的时候由于网络延迟问题导致服务端先行于客户端，直接多次tick(追帧)
            else
            {
                float count = self.HalfRTT * 2 + self.Buffer.Count - aheadFrame;

                while (count-- > 0)
                {
                    self.TickFunc();
                    ++self.CurrentFrame;
                }
                
                // 到这里时 客户端应该领先服务端HalfRTT + Buffer
            }

            // 更改Tick频率
            if (self.HalfRTT * 2 + self.Buffer.Count != aheadFrame)
            {
                //TODO: 更改Update频率
            }
            // 恢复Tick频率
            else
            {
                
            }*/
        }
    }
}