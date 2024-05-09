namespace ET
{
    public abstract class ANodeHandler
    {
        public abstract ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken);
    }
}