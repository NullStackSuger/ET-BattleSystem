using System.Collections.Generic;

namespace ET.Server
{
    [FriendOf(typeof(GameRoomComponent))]
    [FriendOf(typeof(LSFComponent))]
    public static class GameRoomComponentSystem
    {
        public class UpdateSystem : UpdateSystem<GameRoomComponent>
        {
            protected override void Update(GameRoomComponent self)
            {
                if (!self.IsStart) return;
                if (self.Syncs.Count <= 0) return;
                for (int i = self.Syncs.Count - 1; i >= 0; --i)
                {
                    LSFComponent lsf = self.Syncs[i];
                    if (lsf == null || lsf.IsDisposed)
                    {
                        self.Syncs.RemoveAt(i);
                        continue;
                    }
                }
                
                self.Receive();
                self.Tick();
                self.Send();
                
                ++self.Frame;
            }
        }

        public class DestroySystem : DestroySystem<GameRoomComponent>
        {
            protected override void Destroy(GameRoomComponent self)
            {
                self.Syncs?.Clear();
                self.Syncs = null;
            }
        }

        private static void Receive(this GameRoomComponent self)
        {
            foreach (LSFComponent lsf in self.Syncs)
            {
                if (!lsf.Receives.TryGetValue(self.Frame, out SortedSet<LSFCmd> receives)) continue;

                Unit unit = lsf.GetParent<Unit>();
                
                foreach (LSFCmd cmd in receives)
                {
                    LSFCmdHandlerDispatcher.Server[cmd.GetType()]?.Receive(unit, cmd);
                }
                
                lsf.Receives.Remove(self.Frame);
            }
        }

        private static void Send(this GameRoomComponent self)
        {
            M2C_FrameCmd m2CFrameCmd = new();
            SortedSet<LSFCmd> cmds = new();
            foreach (LSFComponent lsf in self.Syncs)
            {
                if (self.AllCmds.TryGetValue(self.Frame, out Queue<LSFCmd> allCmds))
                {
                    foreach (LSFCmd cmd in allCmds)
                    {
                        cmds.Add(cmd);
                    }
                }

                if (lsf.Sends.TryGetValue(self.Frame, out SortedSet<LSFCmd> sends))
                {
                    foreach (LSFCmd cmd in sends)
                    {
                        cmds.Add(cmd);
                    }
                }
                
                //---------------------------------------------------------------------------------
                
                Unit unit = lsf.GetParent<Unit>();
                foreach (LSFCmd cmd in cmds)
                {
                    m2CFrameCmd.Cmd = cmd;
                    NoticeClientHelper.Send(unit, m2CFrameCmd, NoticeClientType.Broad);
                }

                lsf.Sends.Remove(self.Frame);
                cmds.Clear();
            }
        }

        /// <summary>
        /// 这个Tick控制帧率相关
        /// 如TickRate, NeedRollBack
        /// </summary>
        private static void Tick(this GameRoomComponent self)
        {
            List<(LSFComponentHandler, Entity)> handlers = new();
            foreach (LSFComponent lsf in self.Syncs)
            {
                Unit unit = lsf.GetParent<Unit>();
                
                foreach (var pair in unit.Components)
                {
                    if (!LSFComponentHandlerDispatcher.Server.TryGetValue(pair.Key, out var handler)) continue;

                    handlers.Add((handler, pair.Value));
                }
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
        /// 添加一个被帧同步的Unit
        /// </summary>
        public static void TryAddSync(this GameRoomComponent self, Unit unit)
        {
            LSFComponent lsf = null;
            if (!unit.Components.ContainsKey(typeof(LSFComponent)))
                lsf = unit.AddComponent<LSFComponent>();
            else 
                lsf = unit.GetComponent<LSFComponent>();
            
            if (!self.Syncs.Contains(lsf))
                self.Syncs.Add(lsf);
        }

        public static void RecordCmd(this GameRoomComponent self, LSFCmd cmd)
        {
            if (!self.AllCmds.ContainsKey(cmd.Frame))
                self.AllCmds.Add(cmd.Frame, new Queue<LSFCmd>());
            self.AllCmds[cmd.Frame].Enqueue(cmd);
        }
    }
}