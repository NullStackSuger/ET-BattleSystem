//using UnityEngine.Assertions;
using NUnit.Framework;

//using UnityEngine.Assertions;

namespace NPBehave
{
    /// <summary>
    /// 容器负责抽象出子节点停用时，做一些收尾工作的定义
    /// 问自己: DoChildStopped 某个子节点停用并返回执行结果时应该做什么收尾？
    /// </summary>
    public abstract class Container : Node
    {
        private bool collapse = false;
        public bool Collapse
        {
            get
            {
                return collapse;
            }
            set
            {
                collapse = value;
            }
        }

        public Container(string name) : base(name)
        {
        }

        public void ChildStopped(Node child, bool succeeded)
        {
            // Assert.AreNotEqual(this.currentState, State.INACTIVE, "The Child " + child.Name + " of Container " + this.Name + " was stopped while the container was inactive. PATH: " + GetPath());
            Assert.AreNotEqual(this.currentState, State.INACTIVE, "A Child of a Container was stopped while the container was inactive.");
            this.DoChildStopped(child, succeeded);
        }

        protected abstract void DoChildStopped(Node child, bool succeeded);

#if UNITY_EDITOR
        public abstract Node[] DebugChildren
        {
            get;
        }
#endif
    }
}