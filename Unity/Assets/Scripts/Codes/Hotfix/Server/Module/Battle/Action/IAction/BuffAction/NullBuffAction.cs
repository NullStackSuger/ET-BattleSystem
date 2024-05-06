namespace ET.Server
{

    [Action(1001)]
    public class NullBuffAction : IAction
    {
        public bool Check(ActionComponent actionComponent, ActionConfig config)
        {
            return true;
        }

        public async ETTask Run(ActionComponent actionComponent, ActionConfig config, ETCancellationToken cancellationToken)
        {
            Log.Info("TestBuffAction Run");
            await ETTask.CompletedTask;
        }
    }
}