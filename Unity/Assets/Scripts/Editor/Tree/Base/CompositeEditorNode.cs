using GraphProcessor;
using UnityEngine;

namespace ET
{
    public abstract class CompositeEditorNode : EditorNodeBase
    {
        [Input("Input"), Vertical] [HideInInspector]
        public EditorNodeBase Input;
        [Output("OutPut"), Vertical] [HideInInspector]
        public EditorNodeBase OutPut;
        
        public abstract object Init(object[] nodes);
    }
}