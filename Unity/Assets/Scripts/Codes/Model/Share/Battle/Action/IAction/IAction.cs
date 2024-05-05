namespace ET
{

    // 可以考虑在里面放个行为树
    // 在行为树里面放Timeline的Node
    public interface IAction
    {
        bool Check(ActionComponent actionComponent, ActionConfig config);
        ETTask Run(ActionComponent actionComponent, ActionConfig config, ETCancellationToken cancellationToken);
    }
}