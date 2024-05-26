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
            // 获取到当前Player
            //Unit player = self.DomainScene().DomainScene().GetComponent<UnitComponent>()
            // TODO: 获取当前客户端控制的Unit
            Unit player = null;

            // 处理接收到的Cmd
            if (self.Receives.Count > 0)
            {
                Queue<LSFCmd> receives = self.Receives.First().Value;

                while (receives.Count > 0)
                {
                    LSFCmd cmd = receives.Dequeue();

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
                        // 如果预测失败, 需要回滚 追帧
                        foreach (var pair in player.Components)
                        {
                            if (!LSFComponentHandlerDispatcherComponent.Instance.Handlers.TryGetValue(pair.Key, out var handler)) continue;

                            if (!handler.Check(self, pair.Value, cmd))
                            {
                                // 回滚
                                self.RollBack();
                                // 追帧(追到当前帧前一帧就行, 这帧是在下面处理)
                                
                                break;
                            }
                        }
                    }
                }

                self.Receives.Remove(self.CurrentFrame);
            }

            // 调用Tick
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
        private static void RollBack(this LSFComponent self)
        {

        }
    }
}