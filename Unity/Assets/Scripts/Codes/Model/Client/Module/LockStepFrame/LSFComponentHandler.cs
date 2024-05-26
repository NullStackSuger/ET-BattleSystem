using System;

namespace ET.Client
{
    /// <summary>
    /// 监听对应组件数值改变, 发送给LSFComponent
    /// 控制Tick组件相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LSFComponentHandler
    {
        public abstract void TickStart(LSFComponent lsf, Entity component);
        public abstract void Tick(LSFComponent lsf, Entity component);
        public abstract void TickEnd(LSFComponent lsf, Entity component);

        /// <summary>
        /// 检查传入Cmd与当前组件值是否一致
        /// 若不一致 需调用RollBack, 重新预测
        /// </summary>
        public abstract bool Check(LSFComponent lsf, Entity component, LSFCmd cmd);

        public abstract void RollBack(LSFComponent lsf, Entity component, LSFCmd cmd);
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