using System.Collections.Generic;

namespace ET
{
    /// <summary>
    /// 管理所有行为逻辑
    /// 类似EventSystem里管理Update, LateUpdate
    /// </summary>
    [ComponentOf(typeof(Scene))]
    public class ActionsDispatcherComponent : Entity, IAwake, IDestroy, ILoad
    {
        [StaticField]
        public static ActionsDispatcherComponent Instance;

        public Dictionary<int, IAction> Actions = new();
    }
}