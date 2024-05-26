namespace ET.Server
{
    /// <summary>
    /// 监听对应组件数值改变, 发送给LSFComponent
    /// 控制Tick组件相关
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class LSFComponentHandler<T> where T : Entity
    {
        public abstract void TickStart(T component, uint currentFrame /*这里应该有LSFComponent组件作为参数*/);
        public abstract void Tick(T component, uint currentFrame /*这里应该有LSFComponent组件作为参数*/);
        public abstract void TickEnd(T component, uint currentFrame /*这里应该有LSFComponent组件作为参数*/);

        /// <summary>
        /// 检查传入Cmd与当前组件值是否一致
        /// 若不一致 需调用RollBack, 重新预测
        /// </summary>
        public abstract bool Check<K>(T component, uint currentFrame, K cmd /*这里应该有LSFComponent组件作为参数*/) where K : LSFCmd;

        public abstract void RollBack<K>(T component, uint currentFrame, uint triggerFrame, K cmd /*这里应该有LSFComponent组件作为参数*/) where K : LSFCmd;
    }
}