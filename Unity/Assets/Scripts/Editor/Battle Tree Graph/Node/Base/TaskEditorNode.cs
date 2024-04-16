using GraphProcessor;
using NPBehave;
using UnityEngine;
using UnityEngine.UI;

namespace ET
{
    public abstract class TaskEditorNode : EditorNodeBase
    {
        [Input("Input"), Vertical] [HideInInspector]
        public EditorNodeBase Input;
        
        public abstract Task Init(/*ET.Unit unit, */Blackboard blackboard);
    }
}