using Box2DSharp.Dynamics;

namespace ET
{
    [ComponentOf(typeof(Unit))]
    public class B2CollisionComponent : Entity, IAwake<string, Body, Unit>, IDestroy
    {
        public string HandlerName; // 响应碰撞事件的类型名
        public Body Body;
        public Unit Owner; // 所属玩家
    }
}