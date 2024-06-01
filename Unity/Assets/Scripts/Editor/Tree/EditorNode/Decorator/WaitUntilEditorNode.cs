using GraphProcessor;

namespace ET
{
    [NodeMenuItem("Decorator/WaitUntil", typeof(ClientTreeGraph))]
    [NodeMenuItem("Decorator/WaitUntil", typeof(ServerTreeGraph))]
    public class WaitUntilEditorNode : DecoratorEditorNode
    {
        public override object Init(object node)
        {
            this.NodeData = ReflectHelper.CreatNodeData("ET.WaitUntilNodeData");
            return this.NodeData;
        }
    }
}