using GraphProcessor;
using NPBehave;
using UnityEngine;

namespace ET
{
    public abstract class DecoratorEditorNode : EditorNodeBase
    {
        [Input("Input"), Vertical] [HideInInspector]
        public EditorNodeBase Input;
        [Output("OutPut"), Vertical] [HideInInspector]
        public EditorNodeBase OutPut;
        
        public abstract object Init(Blackboard blackboard, object node);
    }
}