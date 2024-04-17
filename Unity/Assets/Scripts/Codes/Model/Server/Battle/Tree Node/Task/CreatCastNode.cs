using NPBehave;

namespace ET.Server
{
    public class CreatCastNode : Task
    {
        private int castConfigId;
        private Unit unit;
        
        public CreatCastNode(int castConfigId, Unit unit) : base("Creat Cast")
        {
            this.castConfigId = castConfigId;
            this.unit = unit;
        }

        protected override void DoStart()
        {
            // 判断unit类型为玩家或怪物
            // 获取CastComponent, 判空
            // 添加Cast
        }

        protected override void DoStop()
        {
            castConfigId = -1;
            unit = null;
        }
    }
}