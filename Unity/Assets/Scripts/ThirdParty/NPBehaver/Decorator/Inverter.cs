/*namespace NPBehave
{
    /// <summary>
    /// 被装饰节点返回后，停用并返回 它取反后的执行结果
    /// </summary>
    public class Inverter : Decorator
    {
        public Inverter(Node decoratee) : base("Inverter", decoratee)
        {
        }

        protected override void DoStart()
        {
            Decoratee.Start();
        }

        override protected void DoStop()
        {
            Decoratee.Stop();
        }

        protected override void DoChildStopped(Node child, bool result)
        {
            Stopped(!result);
        }
    }
}*/