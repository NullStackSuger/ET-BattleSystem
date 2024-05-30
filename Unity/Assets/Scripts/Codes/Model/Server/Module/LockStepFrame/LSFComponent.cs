using System.Collections.Generic;

namespace ET.Server
{
    [ComponentOf(typeof(Unit))]
    public class LSFComponent : Entity, IAwake
    {
        public Dictionary<uint, Queue<LSFCmd>> Sends = new();

        public Dictionary<uint, Queue<LSFCmd>> Receives = new();
    }
}