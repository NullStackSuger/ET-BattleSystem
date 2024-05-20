using System.Collections.Generic;
using System.IO;
using System.Text;
using MongoDB.Bson;

namespace ET
{
    [FriendOf(typeof(TreeComponent))]
    [FriendOf(typeof(NodeDispatcherComponent))]
    public static class TreeComponentSystem
    {
        public class TreeComponentAwakeSystem : AwakeSystem<TreeComponent, string, ETCancellationToken, BlackBoard>
        {
            protected override void Awake(TreeComponent self, string name, ETCancellationToken cancellationToken, BlackBoard blackBoard)
            {
                self.BlackBoard = blackBoard;
                self.CancellationToken = cancellationToken;
                
                self.Load(name);
            }
        }
        public class TreeComponentAwakeSystem1 : AwakeSystem<TreeComponent, string>
        {
            protected override void Awake(TreeComponent self, string name)
            {
                self.BlackBoard = new();
                self.CancellationToken = new();
                
                self.Load(name);
            }
        }

        public class TreeComponentDestroySystem : DestroySystem<TreeComponent>
        {
            protected override void Destroy(TreeComponent self)
            {
                self.Stop();
            }
        }
        
        private static void Load(this TreeComponent self, string name)
        {
            if (TreeComponent.AlreadyLoadTree.ContainsKey(name))
            {
                Log.Info($"已经加载过{name}");
                self.AddComponent(TreeComponent.AlreadyLoadTree[name]);
            }
            else
            {
                Log.Info($"未加载过{name}, 准备开始加载");
                byte[] file = File.ReadAllBytes($"{TreeComponent.TreeFilePath}/{name}.bytes");
                if (file.Length == 0) Log.Error("没有读取到文件");
                    
                RootNodeData rootData = MongoHelper.Deserialize<RootNodeData>(file);
                Log.Info($"反序列化{name}.bytes 成功");
                    
                // -------------------------------------------------------------------------------------
                // 遍历AddNode
                AddNode(self, rootData);

                // 完成初始化
                TreeComponent.AlreadyLoadTree.Add(name, self.Root);

                void AddNode(Entity parent, BaseNodeData data)
                {
                    Entity current = data.AddNode(parent, self);
                    Log.Info($"添加节点{data.GetType()}");
                    
                    switch (data)
                    {
                        case TaskNodeData taskNodeData:
                            return;
                        case DecoratorNodeData decoratorNodeData:
                            Log.Info($"{decoratorNodeData.GetType()} Child is {decoratorNodeData.Child.GetType()}");
                            AddNode(current, decoratorNodeData.Child);
                            break;
                        case CompositeNodeData compositeNodeData:
                            foreach (BaseNodeData child in compositeNodeData.Children)
                            {
                                Log.Info($"{compositeNodeData.GetType()} Child is {child.GetType()}");
                                AddNode(current, child);
                            }
                            break;
                    }
                }
            }
        }
        
        public static async ETTask<bool> Start(this TreeComponent self, string name = "")
        {
            if (self.Root == null) self.Load(name);
            self.CancellationToken ??= new();
            
            return await NodeDispatcherComponent.Instance.NodeHandlers[self.Root.GetType()].Run(self.Root, self, self.CancellationToken);
        }

        public static void Stop(this TreeComponent self)
        {
            self.CancellationToken?.Cancel();
            self.CancellationToken = null;
            self.BlackBoard.Clear();
            
            self.RemoveComponent<RootNode>();
        }
    }
}