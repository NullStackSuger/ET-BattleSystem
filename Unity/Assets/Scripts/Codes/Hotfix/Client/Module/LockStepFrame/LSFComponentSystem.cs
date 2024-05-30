using System.Collections.Generic;
using System.Linq;

namespace ET.Client
{
    [FriendOfAttribute(typeof(ET.Client.LSFComponent))]
    public static class LSFComponentSystem
    {
        public class LSFComponentAwakeSystem : AwakeSystem<LSFComponent>
        {
            protected override void Awake(LSFComponent self)
            {

            }
        }

        /// <summary>
        /// 把cmd加入待发送队列
        /// </summary>
        public static void AddToSend(this LSFComponent self, LSFCmd cmd)
        {
            if (!self.Sends.ContainsKey(cmd.Frame))
                self.Sends.Add(cmd.Frame, new Queue<LSFCmd>());
            self.Sends[cmd.Frame].Enqueue(cmd);
        }

        /// <summary>
        /// 把cmd加入待处理队列
        /// </summary>
        public static void AddToReceive(this LSFComponent self, LSFCmd cmd)
        {
            if (!self.Receives.ContainsKey(cmd.Frame))
                self.Receives.Add(cmd.Frame, new Queue<LSFCmd>());
            self.Receives[cmd.Frame].Enqueue(cmd);
        }
    }
}