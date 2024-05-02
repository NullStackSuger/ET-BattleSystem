//using UnityEngine.Assertions;

using System;
using MongoDB.Bson.Serialization.Attributes;
using UnityEngine;

namespace NPBehave
{
    public class Root : Decorator
    {
        [BsonRequired]
        private Node mainNode;

        //private Node inProgressNode;
        [BsonRequired]
        private Blackboard blackboard;
        public override Blackboard Blackboard
        {
            get
            {
                return blackboard;
            }
        }
        [BsonRequired]
        private Clock clock;
        public override Clock Clock
        {
            get
            {
                return clock;
            }
        }

/*#if UNITY_EDITOR
        public int TotalNumStartCalls = 0;
        public int TotalNumStopCalls = 0;
        public int TotalNumStoppedCalls = 0;
#endif*/

        public Root(Node mainNode) : base("Root", mainNode)
        {
            this.mainNode = mainNode;
            this.clock = UnityContext.GetClock();
            this.blackboard = new Blackboard(this.clock);
            this.SetRoot(null);
        }
        public Root(Blackboard blackboard, Node mainNode) : base("Root", mainNode)
        {
            this.blackboard = blackboard;
            this.mainNode = mainNode;
            this.clock = UnityContext.GetClock();
            this.SetRoot(null);
        }

        public Root(Blackboard blackboard, Clock clock, Node mainNode) : base("Root", mainNode)
        {
            this.blackboard = blackboard;
            this.mainNode = mainNode;
            this.clock = clock;
            this.SetRoot(null);
        }

        public override void SetRoot(Root rootNode)
        {
            //Assert.AreEqual(this, rootNode);
            base.SetRoot(rootNode);
            this.mainNode.SetRoot(rootNode);
        }


        override protected void DoStart()
        {
            this.Blackboard.Enable();
            this.mainNode.Start();
        }

        override protected void DoStop()
        {
            if (this.mainNode.IsActive)
            {
                this.mainNode.Stop();
            }
            else
            {
                this.clock.RemoveTimer(this.mainNode.Start);
            }
        }


        override protected void DoChildStopped(Node node, bool success)
        {
            if (!IsStopRequested)
            {
                // wait one tick, to prevent endless recursions
                this.clock.AddTimer(0, 0, this.mainNode.Start);
            }
            else
            {
                this.Blackboard.Disable();
                Stopped(success);
            }
        }
    }
}
