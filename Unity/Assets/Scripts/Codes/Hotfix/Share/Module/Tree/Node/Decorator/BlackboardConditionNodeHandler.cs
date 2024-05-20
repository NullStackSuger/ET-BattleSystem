namespace ET
{
    [NodeHandler(typeof(BlackboardConditionNode))]
    [FriendOf(typeof(BlackboardConditionNode))]
    [FriendOf(typeof(NodeDispatcherComponent))]
    [FriendOf(typeof(TreeComponent))]
    public class BlackboardConditionNodeHandler : ANodeHandler
    {
        public override async ETTask<bool> Run(Entity iNode, TreeComponent tree, ETCancellationToken cancellationToken)
        {
            var node = iNode as BlackboardConditionNode;
            
            if (CheckValue(node.Op, tree.BlackBoard[node.Key], node.Value))
            {
                // 添加监听
                node.childCancel = new();
                cancellationToken.Add(ChildCancel);
                tree.BlackBoard.AddObserver(node.Key, OnValueChanged);
                
                // 运行子节点
                bool res = await NodeDispatcherComponent.Instance.NodeHandlers[node.Child.GetType()].Run(node.Child, tree, node.childCancel);
                
                // 移除监听
                tree.BlackBoard.RemoveObserver(node.Key, OnValueChanged);
                cancellationToken.Remove(ChildCancel);
                node.childCancel = null;
                
                return res;
            }
            else
            {
                return false;
            }


            void ChildCancel()
            {
                node.childCancel?.Cancel();
            }
            void OnValueChanged(string key, object oldValue, object newValue)
            {
                // 不满足条件 && 当前运行节点 == 子节点
                if (!CheckValue(node.Op, tree.BlackBoard[node.Key], node.Value))
                {
                    ChildCancel();
                }
            }
        }

        private bool CheckValue(Operator op, object value1, object value2)
        {
            bool result = false;
            switch (op)
            {
                case Operator.Equal:
                    result = value1 == value2;
                    break;
                case Operator.NotEqual:
                    result = value1 != value2;
                    break;
                case Operator.Smaller:
                    if (value1 is float && value2 is float)
                        result = (float)value1 < (float)value2;
                    else if (value1 is int && value2 is int)
                        result = (int)value1 < (int)value2;
                    else result = false;
                    break;
                case Operator.SmallerOrEqual:
                    if (value1 is float && value2 is float)
                        result = (float)value1 <= (float)value2;
                    else if (value1 is int && value2 is int)
                        result = (int)value1 <= (int)value2;
                    else result = false;
                    break;
                case Operator.Greater:
                    if (value1 is float && value2 is float)
                        result = (float)value1 > (float)value2;
                    else if (value1 is int && value2 is int)
                        result = (int)value1 > (int)value2;
                    else result = false;
                    break;
                case Operator.GreaterOrEqual:
                    if (value1 is float && value2 is float)
                        result = (float)value1 >= (float)value2;
                    else if (value1 is int && value2 is int)
                        result = (int)value1 >= (int)value2;
                    else result = false;
                    break;
            }

            return result;
        }
    }
    
    

    public class BlackboardConditionNodeAwakeSystem : AwakeSystem<BlackboardConditionNode, Operator, string, object>
    {
        protected override void Awake(BlackboardConditionNode self, Operator op, string key, object value)
        {
            self.Op = op;
            self.Key = key;
            self.Value = value;
        }
    }
    
    public class BlackboardConditionNodeDestroySystem : DestroySystem<BlackboardConditionNode>
    {
        protected override void Destroy(BlackboardConditionNode self)
        {
            self.Key = "";
            self.Value = null;
        }
    }
}