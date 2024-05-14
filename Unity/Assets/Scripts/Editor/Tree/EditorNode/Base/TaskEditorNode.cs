using System.Collections.Generic;
using GraphProcessor;
using UnityEngine;

namespace ET
{
    public abstract class TaskEditorNode : EditorNodeBase
    {
        [Input("Input"), Vertical] [HideInInspector]
        public EditorNodeBase Input;
        
        public abstract object Init();
    }
}