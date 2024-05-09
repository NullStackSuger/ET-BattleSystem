using System.IO;

namespace ET
{
    [FriendOf(typeof(TreeComponent))]
    public static class TreeComponentSystem
    {
        public class TreeComponentAwakeSystem : AwakeSystem<TreeComponent, string>
        {
            protected override void Awake(TreeComponent self, string treeName)
            {
                self.ChangeTree(treeName);
            }
        }

        public class TreeComponentDestroySystem : DestroySystem<TreeComponent>
        {
            protected override void Destroy(TreeComponent self)
            {
                self.CancellationToken?.Cancel();
                self.CancellationToken = null;
                self.Root = null;
            }
        }

        public static void ChangeTree(this TreeComponent self, string treeName)
        {
            self.CancellationToken?.Cancel();
            self.CancellationToken = new();

            if (TreeComponent.AlreadyLoadTree.ContainsKey(treeName))
                self.Root = TreeComponent.AlreadyLoadTree[treeName];
            else
            {
                byte[] file = File.ReadAllBytes($"{TreeComponent.TreeFilePath}/{treeName}.bytes");
                if (file.Length == 0) Log.Error("没有读取到文件");
                
                RootNodeData rootData = MongoHelper.Deserialize<RootNodeData>(file);
                Log.Info($"反序列化{treeName}.bytes 成功");
                
                // TODO: 根据RootNodeData构建RootNode
                RootNode root = null;

                // 完成初始化
                self.Root = root;
                TreeComponent.AlreadyLoadTree.Add(treeName, root);
            }
        }

        public static async void Start(this TreeComponent self)
        {
            if (self.Root == null)
            {
                Log.Error("TreeComponent.Root不存在");
                return;
            }

            await NodeDispatcherComponent.Instance.Get(self.Root.GetType()).Run(self.Root, self, self.CancellationToken);
        }

        public static void Stop(this TreeComponent self)
        {
            self.CancellationToken?.Cancel();
            self.CancellationToken = new();
        }
    }
}