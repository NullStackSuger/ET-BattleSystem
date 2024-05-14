using System;

namespace ET
{
    [FriendOf(typeof (ActionComponent))]
    public static class ActionComponentSystem
    {
        public class ActionComponentAwakeSystem: AwakeSystem<ActionComponent>
        {
            protected override void Awake(ActionComponent self)
            {
                self.Actions = new();
            }
        }

        public class ActionComponentDestroySystem: DestroySystem<ActionComponent>
        {
            protected override void Destroy(ActionComponent self)
            {
                self.Actions.Clear();
            }
        }

        public static void Creat(this ActionComponent self, string name)
        {
            TreeComponent tree = self.AddChild<TreeComponent, string>(name);
            tree.Start().Coroutine();
            self.Actions.Add(name, tree);
        }

        public static void Remove(this ActionComponent self, string name)
        {
            if (self.Current == name)
            {
                self.Get(name).Stop();
            }
            
            self.RemoveChild(self.Get(name).Id);
            self.Actions.Remove(name);
        }

        public static TreeComponent Get(this ActionComponent self, string name)
        {
            return self.Actions[name];
        }
    }
}