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
            bool res = false;
            
            switch (node.Op)
            {
                case Operator.Equal:
                    res = tree.BlackBoard.Get<object>(node.Key) == node.Value;
                    break;
                case Operator.NotEqual:
                    res = tree.BlackBoard.Get<object>(node.Key) != node.Value;
                    break;
                case Operator.Smaller:
                    if (node.Value is float)
                        res = tree.BlackBoard.Get<float>(node.Key) < (float)node.Value;
                    else if (node.Value is int)
                        res = tree.BlackBoard.Get<int>(node.Key) < (int)node.Value;
                    else res = false;
                    break;
                case Operator.SmallerOrEqual:
                    if (node.Value is float)
                        res = tree.BlackBoard.Get<float>(node.Key) <= (float)node.Value;
                    else if (node.Value is int)
                        res = tree.BlackBoard.Get<int>(node.Key) <= (int)node.Value;
                    else res = false;
                    break;
                case Operator.Greater:
                    if (node.Value is float)
                        res = tree.BlackBoard.Get<float>(node.Key) > (float)node.Value;
                    else if (node.Value is int)
                        res = tree.BlackBoard.Get<int>(node.Key) > (int)node.Value;
                    else res = false;
                    break;
                case Operator.GreaterOrEqual:
                    if (node.Value is float)
                        res = tree.BlackBoard.Get<float>(node.Key) >= (float)node.Value;
                    else if (node.Value is int)
                        res = tree.BlackBoard.Get<int>(node.Key) >= (int)node.Value;
                    else res = false;
                    break;
            }

            if (res)
            {
                return await NodeDispatcherComponent.Instance.NodeHandlers[node.Child.GetType()].Run(node.Child, tree, cancellationToken);
            }
            
            return false;
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