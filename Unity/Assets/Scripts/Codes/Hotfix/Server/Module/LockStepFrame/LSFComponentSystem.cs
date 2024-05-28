using System;
using System.Collections.Generic;

namespace ET.Server
{
    [FriendOfAttribute(typeof(ET.Server.LSFComponent))]
    [FriendOfAttribute(typeof(ET.Server.LSFCmdHandlerDispatcherComponent))]
    [FriendOfAttribute(typeof(ET.Server.LSFComponentHandlerDispatcherComponent))]
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
        /// <param name="self"></param>
        private static void Tick(this LSFComponent self)
        {
            // 处理接收到的Cmd
            if (self.Receives.TryGetValue(self.CurrentFrame, out Queue<LSFCmd> receives))
            {
                while (receives.Count > 0)
                {
                    LSFCmd cmd = receives.Dequeue();
                    LSFCmdHandlerDispatcherComponent.Instance.Handlers[cmd.GetType()]?.Receive(cmd);
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
                M2C_FrameCmd m2CFrameCmd = new()
                {
                    Frame = self.CurrentFrame,
                };
                while (sends.Count > 0)
                {
                    LSFCmd cmd = sends.Dequeue();
                    m2CFrameCmd.Cmd = cmd;
                    
                    NoticeClientHelper.Send(Root.Instance.Scene.GetComponent<UnitComponent>().Get(cmd.UnitId), m2CFrameCmd, NoticeClientType.Broad);
                }

                self.Sends.Remove(self.CurrentFrame);
            }

            ++self.CurrentFrame;
        }
    }
}