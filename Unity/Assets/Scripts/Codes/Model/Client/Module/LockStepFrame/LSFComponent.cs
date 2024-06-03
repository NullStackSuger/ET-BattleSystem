using System.Collections.Generic;

namespace ET.Client
{
    [ComponentOf(typeof(Unit))]
    public class LSFComponent : Entity, IAwake
    {
        public Dictionary<uint, SortedSet<LSFCmd>> Sends = new();

        public Dictionary<uint, SortedSet<LSFCmd>> Receives = new();
    }
}