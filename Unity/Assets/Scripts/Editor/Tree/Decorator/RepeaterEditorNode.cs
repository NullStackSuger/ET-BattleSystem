using GraphProcessor;
using NPBehave;
using Sirenix.OdinInspector;

namespace ET
{
    [NodeMenuItem("Tree/Decorator/Repeater", typeof(TreeGraph))]
    public class RepeaterEditorNode : DecoratorEditorNode
    {
        private int loopCount = -1;
        
        public override object Init(Blackboard blackboard, object node)
        {
            this.NodeData = NodeHelper.CreatNodeData("ET.RepeaterNodeData", 
                ("loopCount", this.loopCount));
            return this.NodeData;
        }
    }
}