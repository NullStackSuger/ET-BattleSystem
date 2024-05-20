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
            bool meetCondition = false;
            
            switch (node.Op)
            {
                case Operator.Equal:
                    meetCondition = tree.BlackBoard.Get<object>(node.Key) == node.Value;
                    break;
                case Operator.NotEqual:
                    meetCondition = tree.BlackBoard.Get<object>(node.Key) != node.Value;
                    break;
                case Operator.Smaller:
                    if (node.Value is float)
                        meetCondition = tree.BlackBoard.Get<float>(node.Key) < (float)node.Value;
                    else if (node.Value is int)
                        meetCondition = tree.BlackBoard.Get<int>(node.Key) < (int)node.Value;
                    else meetCondition = false;
                    break;
                case Operator.SmallerOrEqual:
                    if (node.Value is float)
                        meetCondition = tree.BlackBoard.Get<float>(node.Key) <= (float)node.Value;
                    else if (node.Value is int)
                        meetCondition = tree.BlackBoard.Get<int>(node.Key) <= (int)node.Value;
                    else meetCondition = false;
                    break;
                case Operator.Greater:
                    if (node.Value is float)
                        meetCondition = tree.BlackBoard.Get<float>(node.Key) > (float)node.Value;
                    else if (node.Value is int)
                        meetCondition = tree.BlackBoard.Get<int>(node.Key) > (int)node.Value;
                    else meetCondition = false;
                    break;
                case Operator.GreaterOrEqual:
                    if (node.Value is float)
                        meetCondition = tree.BlackBoard.Get<float>(node.Key) >= (float)node.Value;
                    else if (node.Value is int)
                        meetCondition = tree.BlackBoard.Get<int>(node.Key) >= (int)node.Value;
                    else meetCondition = false;
                    break;
            }

            if (meetCondition)
            {
                // 添加监听, 当值改变 && 不满足时, 调用自己Stop()
                // 监听最后移除监听
                tree.BlackBoard.AddObserver(node.Key, OnValueChanged);
                bool res = await NodeDispatcherComponent.Instance.NodeHandlers[node.Child.GetType()].Run(node.Child, tree, cancellationToken);
                // 移除监听
                tree.BlackBoard.RemoveObserver(node.Key, OnValueChanged);
                return res;
            }
            else
            {
                return false;
            }
            
            
            void OnValueChanged(string key, object oldValue, object newValue)
            {
                tree.Stop();
                tree.BlackBoard.RemoveObserver(node.Key, OnValueChanged);
                tree.Start().Coroutine();
            }
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