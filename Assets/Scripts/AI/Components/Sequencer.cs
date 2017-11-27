using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI.Components
{
    public class Sequencer : BehaviorComponent
    {
        public Sequencer(string name, int depth, int id) 
            : base(name, depth, id)
        {
        }

        public override IEnumerator Tick()
        {
            bool childRunning = false;

            foreach (var behaviorRun in RunningChildren)
            {
                this.BehaviorTreeManager.StartCoroutine(behaviorRun.Tick());
                if (behaviorRun.CurrentState != BehaviorState.Running)
                {
                    FinishedRunningChildren.Add(behaviorRun);
                }
            }

            foreach (var behavior in SubBehaviors)
            {
                if (behavior.CurrentState == BehaviorState.Running ||
                    FinishedRunningChildren.Contains(behavior)) continue;
                this.BehaviorTreeManager.StartCoroutine(behavior.Tick());
                switch (behavior.CurrentState)
                {
                    case BehaviorState.Fail:
                        this.CurrentState = BehaviorState.Fail;
                        break;
                    case BehaviorState.Success:
                        CurrentState = BehaviorState.Running;
                        continue;
                    case BehaviorState.Running:
                        CurrentState = BehaviorState.Running;
                        childRunning = true;
                        this.RunningChildren.Add(behavior);
                        continue;
                    default:
                        Debug.LogError("Not a valid BehaviorState.");
                        break;
                }
            }

            RunningChildren.RemoveWhere(a => FinishedRunningChildren.Contains(a));
            this.CurrentState = !childRunning ? BehaviorState.Success : BehaviorState.Running;
            yield return null;
        }
    }

}