namespace ET
{
    public class B2CollisionHandlerAttribute: BaseAttribute
    {
        
    }

    [B2CollisionHandler]
    public abstract class AB2CollisionHandler
    {
        public abstract void CollisionStart(Unit a, Unit b);

        public abstract void CollisionSustain(Unit a, Unit b);

        public abstract void CollisionEnd(Unit a, Unit b);
    }
}