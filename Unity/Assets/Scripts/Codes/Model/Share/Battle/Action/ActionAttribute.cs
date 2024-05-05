namespace ET
{

    /// <summary>
    /// 应该是和IAction配合
    /// ActionsDispatcherComponent获取所有特性来管理IAction
    /// </summary>
    public class ActionAttribute: BaseAttribute
    {
        public int ConfigId { get; }

        public ActionAttribute(int configId)
        {
            this.ConfigId = configId;
        }
    }
}