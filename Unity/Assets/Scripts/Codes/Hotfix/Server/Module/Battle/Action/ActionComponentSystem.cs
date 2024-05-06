using System;

namespace ET.Server
{
    [FriendOf(typeof (ActionComponent))]
    public static class ActionComponentSystem
    {
        public class ActionComponentAwakeSystem: AwakeSystem<ActionComponent>
        {
            protected override void Awake(ActionComponent self)
            {
                self.Actions?.Clear();
                self.Actions ??= new();
            }
        }

        public class ActionComponentDestorySystem: DestroySystem<ActionComponent>
        {
            protected override void Destroy(ActionComponent self)
            {
                self.Cancel();
                self.Actions.Clear();
            }
        }

        public class ActionComponentUpdateSystem: UpdateSystem<ActionComponent>
        {
            protected override void Update(ActionComponent self)
            {
                foreach (int actionId in self.Actions)
                {
                    ActionConfig config = ActionConfigCategory.Instance.Get(actionId);
                    IAction action = self.Get(actionId);
                    
                    if (!action.Check(self, config)) continue;
                    if (actionId == self.Current) continue;
                    
                    self.Cancel();
                    self.CancellationToken = new();
                    self.Current = actionId;
                    action.Run(self, config, self.CancellationToken).Coroutine();
                    return;
                }
            }
        }

        public static void Creat(this ActionComponent self, int configId)
        {
            self.Actions.Insert(0,configId);
        }

        public static void Remove(this ActionComponent self, int configId)
        {
            if (self.Current == configId)
            {
                self.Cancel();
            }
            self.Actions.Remove(configId);
        }

        public static IAction Get(this ActionComponent self, int configId)
        {
            if (self.Actions.Contains(configId))
                return ActionsDispatcherComponent.Instance.Get(configId);
            else return null;
        }

        private static void Cancel(this ActionComponent self)
        {
            self.CancellationToken?.Cancel();
            self.CancellationToken = null;
            self.Current = int.MinValue;
        }
    }
}