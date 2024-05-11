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
        public class TreeComponentAwakeSystem : AwakeSystem<TreeComponent, string>
        {
            protected override void Awake(TreeComponent self, string treeName)
            {
                // 为TreeComponent构建组件
                // 加载过对应树时, 说明已经构建好了, 直接添加就行
                if (TreeComponent.AlreadyLoadTree.ContainsKey(treeName))
                {
                    Log.Info($"已经加载过{treeName}");
                    self.AddComponent(TreeComponent.AlreadyLoadTree[treeName]);
                }
                else // 没加载过对应组件时, 先加载, 再构建
                {
                    Log.Info($"未加载过{treeName}, 准备开始加载");
                    byte[] file = File.ReadAllBytes($"{TreeComponent.TreeFilePath}/{treeName}.bytes");
                    if (file.Length == 0) Log.Error("没有读取到文件");
                    
                    RootNodeData rootData = MongoHelper.Deserialize<RootNodeData>(file);
                    Log.Info($"反序列化{treeName}.bytes 成功");
                    
                    // -------------------------------------------------------------------------------------
                    // 遍历AddNode
                    AddNode(self, rootData);

                    // 完成初始化
                    TreeComponent.AlreadyLoadTree.Add(treeName, self.GetComponent<RootNode>());

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
        }

        public class TreeComponentDestroySystem : DestroySystem<TreeComponent>
        {
            protected override void Destroy(TreeComponent self)
            {
                self.CancellationToken?.Cancel();
                self.CancellationToken = null;
            }
        }

        public static async void Start(this TreeComponent self)
        {
            if (self.Root == null)
            {
                Log.Error("TreeComponent.Root不存在");
                return;
            }

            await NodeDispatcherComponent.Instance.NodeHandlers[self.Root.GetType()].Run(self.Root, self, self.CancellationToken);
        }

        public static void Stop(this TreeComponent self)
        {
            self.CancellationToken?.Cancel();
            self.CancellationToken = new();
        }
    }
}