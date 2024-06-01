using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Decorator/BlackboardCondition", typeof(ClientTreeGraph))]
    [NodeMenuItem("Decorator/BlackboardCondition", typeof(ServerTreeGraph))]
    public class BlackboardConditionEditorNode : DecoratorEditorNode
    {
        public enum Operator
        {
            Equal, NotEqual,
            Smaller, SmallerOrEqual,
            Greater, GreaterOrEqual
        }

        public Operator Op;
        public string Key;
        public object Value;
        
        
        public override object Init(object node)
        {
            this.NodeData = ReflectHelper.CreatNodeData("ET.BlackboardConditionNodeData");
            ReflectHelper.SetField(this.NodeData, ("Op", (int)this.Op));
            ReflectHelper.SetField(this.NodeData, ("Key", this.Key));
            ReflectHelper.SetField(this.NodeData, ("Value", this.Value));
            return this.NodeData;
        }
    }
}