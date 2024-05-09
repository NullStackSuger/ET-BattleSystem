using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace ET
{
    public abstract class DecoratorEditorNode : EditorNodeBase
    {
        [Input("Input"), Vertical] [HideInInspector]
        public EditorNodeBase Input;
        [Output("OutPut"), Vertical] [HideInInspector]
        public EditorNodeBase OutPut;
        
        public abstract object Init(Dictionary<string, object> blackboard, object node);
    }
}