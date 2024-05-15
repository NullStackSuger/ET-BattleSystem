using System.Linq;
using MongoDB.Bson.Serialization.Attributes;

namespace ET
{

    public class SubTreeNode: Entity, INode, IAwake<string, ETCancellationToken, BlackBoard>, IDestroy
    {
        public string Name;

        [BsonIgnore]
        public TreeComponent SubTree
        {
            get
            {
                return this.Children.Values.First() as TreeComponent;
            }
        }
    }
}