namespace ET
{
    namespace Node
    {
        /// <summary>
        /// 等待直到被停用
        /// </summary>
        public class WaitUntilStopped: Task
        {
            private bool sucessWhenStopped;

            public WaitUntilStopped(bool sucessWhenStopped = false): base("WaitUntilStopped")
            {
                this.sucessWhenStopped = sucessWhenStopped;
            }

            protected override void DoStop()
            {
                this.Stopped(sucessWhenStopped);
            }
        }
    }
}