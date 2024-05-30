using System;

namespace ET.Server
{
    /// <summary>
    /// 监听对应组件数值改变, 发送给LSFComponent
    /// 控制Tick组件相关
    /// </summary>
    public abstract class LSFComponentHandler
    {
        public abstract void TickStart(GameRoomComponent room, Entity component);
        public abstract void Tick(GameRoomComponent room, Entity component);
        public abstract void TickEnd(GameRoomComponent room, Entity component);
    }
    
    public class LSFComponentHandlerAttribute: BaseAttribute
    {
        public Type Type;
        public LSFComponentHandlerAttribute(Type type)
        {
            this.Type = type;
        }
    }
}