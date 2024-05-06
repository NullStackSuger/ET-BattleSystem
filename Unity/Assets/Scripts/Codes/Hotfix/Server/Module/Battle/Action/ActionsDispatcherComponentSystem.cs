using System;
using System.Collections.Generic;

namespace ET.Server
{
    public class ActionsDispatcherComponentAwakeSystem: AwakeSystem<ActionsDispatcherComponent>
    {
        protected override void Awake(ActionsDispatcherComponent self)
        {
            ActionsDispatcherComponent.Instance = self;
            self.Refresh();
        }
    }
    
    public class ActionsDispatcherComponentDestorySystem: DestroySystem<ActionsDispatcherComponent>
    {
        protected override void Destroy(ActionsDispatcherComponent self)
        {
            ActionsDispatcherComponent.Instance = null;
            self.Actions.Clear();
        }
    }
    
    public class ActionsDispatcherComponentLoadSystem: LoadSystem<ActionsDispatcherComponent>
    {
        protected override void Load(ActionsDispatcherComponent self)
        {
            self.Refresh();
        }
    }
    
    [FriendOf(typeof(ActionsDispatcherComponent))]
    public static class ActionsDispatcherComponentSystem
    {
        public static void Refresh(this ActionsDispatcherComponent self)
        {
            self.Actions.Clear();
            var types = EventSystem.Instance.GetTypes(typeof(ActionAttribute));
            foreach (Type type in types)
            {
                var attrs = type.GetCustomAttributes(typeof (ActionAttribute), false);
                if (attrs.Length <= 0) continue;
                
                ActionAttribute attribute = attrs[0] as ActionAttribute;
                IAction iaction = Activator.CreateInstance(type) as IAction;
                
                if (iaction == null) throw new Exception(type + "not inherit IActions");

                self.Actions.Add(attribute.ConfigId, iaction);
            }
        }
        
        public static IAction Get(this ActionsDispatcherComponent self, int configId)
        {
            return self.Actions.GetValueOrDefault(configId);
        }
    }
}