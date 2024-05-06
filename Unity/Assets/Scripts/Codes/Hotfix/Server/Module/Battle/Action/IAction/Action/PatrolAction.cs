namespace ET.Server
{

    [Action(2001)]
    public class PatrolAction: IAction
    {
        public bool Check(ActionComponent actionComponent, ActionConfig config)
        {
            return true;
        }

        public async ETTask Run(ActionComponent actionComponent, ActionConfig config, ETCancellationToken cancellationToken)
        {
            // 获取行为参数

            // 由参数获取巡逻路径

            // 调用MoveComponent

            await ETTask.CompletedTask;
        }
    }
}