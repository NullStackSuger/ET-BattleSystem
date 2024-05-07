
using System;
using NPBehave;
using NUnit.Framework;

namespace ET
{
    namespace Node
    {

        /// <summary>
        /// 问自己: DoStart 开启时应该做什么？
        ///        DoStop 关闭时应该做什么？
        ///        DoParentCompositeStopped 父组合节点关闭时应该做什么？
        /// </summary>
        public abstract class Node/* : Entity*/
        {
            public enum State
            {
                INACTIVE,
                ACTIVE,
                STOP_REQUESTED,
            }

            public State CurrentState { get; protected set; }
            public Container ParentNode { get; private set; }

            /*#if UNITY_EDITOR
                    public float DebugLastStopRequestAt = 0.0f;
                    public float DebugLastStoppedAt = 0.0f;
                    public int DebugNumStartCalls = 0;
                    public int DebugNumStopCalls = 0;
                    public int DebugNumStoppedCalls = 0;
                    public bool DebugLastResult = false;
            #endif*/

            public void Start()
            {
                // Assert.AreEqual(this.currentState, State.INACTIVE, "can only start inactive nodes, tried to start: " + this.Name + "! PATH: " + GetPath());
                Assert.AreEqual(this.currentState, State.INACTIVE, "can only start inactive nodes");

                /*#if UNITY_EDITOR
                RootNode.TotalNumStartCalls++;
                this.DebugNumStartCalls++;
                #endif*/
                this.currentState = State.ACTIVE;
                DoStart();
            }

            /// <summary>
            /// TODO: Rename to "Cancel" in next API-Incompatible version
            /// </summary>
            public void Stop()
            {
                // Assert.AreEqual(this.currentState, State.ACTIVE, "can only stop active nodes, tried to stop " + this.Name + "! PATH: " + GetPath());
                Assert.AreEqual(this.currentState, State.ACTIVE, "can only stop active nodes, tried to stop");
                this.currentState = State.STOP_REQUESTED;
                /*#if UNITY_EDITOR
                            RootNode.TotalNumStopCalls++;
                            this.DebugLastStopRequestAt = UnityEngine.Time.time;
                            this.DebugNumStopCalls++;
                #endif*/
                DoStop();
            }

            protected virtual void DoStart()
            {

            }

            protected virtual void DoStop()
            {

            }


            /// THIS ABSOLUTLY HAS TO BE THE LAST CALL IN YOUR FUNCTION, NEVER MODIFY
            /// ANY STATE AFTER CALLING Stopped !!!!
            protected virtual void Stopped(bool success)
            {
                // Assert.AreNotEqual(this.currentState, State.INACTIVE, "The Node " + this + " called 'Stopped' while in state INACTIVE, something is wrong! PATH: " + GetPath());
                Assert.AreNotEqual(this.currentState, State.INACTIVE, "Called 'Stopped' while in state INACTIVE, something is wrong!");
                this.currentState = State.INACTIVE;
                /*#if UNITY_EDITOR
                            RootNode.TotalNumStoppedCalls++;
                            this.DebugNumStoppedCalls++;
                            this.DebugLastStoppedAt = UnityEngine.Time.time;
                            DebugLastResult = success;
                #endif*/
                if (this.ParentNode != null)
                {
                    this.ParentNode.ChildStopped(this, success);
                }
            }

            public virtual void ParentCompositeStopped(Composite composite)
            {
                DoParentCompositeStopped(composite);
            }

            /// THIS IS CALLED WHILE YOU ARE INACTIVE, IT's MEANT FOR DECORATORS TO REMOVE ANY PENDING
            /// OBSERVERS
            protected virtual void DoParentCompositeStopped(Composite composite)
            {
                /// be careful with this!
            }

            // public Composite ParentComposite
            // {
            //     get
            //     {
            //         if (ParentNode != null && !(ParentNode is Composite))
            //         {
            //             return ParentNode.ParentComposite;
            //         }
            //         else
            //         {
            //             return ParentNode as Composite;
            //         }
            //     }
            // }

            override public string ToString()
            {
                return !string.IsNullOrEmpty(Label)? (this.Name + "{" + Label + "}") : this.Name;
            }

            protected string GetPath()
            {
                if (ParentNode != null)
                {
                    return ParentNode.GetPath() + "/" + Name;
                }
                else
                {
                    return Name;
                }
            }
        }
    }
}