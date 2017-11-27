using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.AI
{
    public class BehaviorManager : MonoBehaviour
    {
        [SerializeField]
        public BehaviorTreeElement[] SubBehaviors;
        private HashSet<BehaviorTreeElement> RunningChildren = new HashSet<BehaviorTreeElement>();
        private HashSet<BehaviorTreeElement> FinishedRunningChildren = new HashSet<BehaviorTreeElement>();

        //TODO:: This needs to return to the first element whenever a sub element fails
        //       and there are no more running sub-tasks.

        void Update()
        {
            bool childRunning = false;
            foreach (var behaviorRun in RunningChildren)
            {
                StartCoroutine(behaviorRun.Tick());
                if(behaviorRun.CurrentState != BehaviorState.Running)
                {
                    FinishedRunningChildren.Add(behaviorRun);
                    RunningChildren.Remove(behaviorRun);
                }
            }

            foreach (var behavior in SubBehaviors)
            {
                if (behavior.CurrentState == BehaviorState.Running ||
                    FinishedRunningChildren.Contains(behavior)) continue;
                StartCoroutine(behavior.Tick());

                switch (behavior.CurrentState)
                {
                    case BehaviorState.Fail:
                        //need to stop or finish all running tasks and restart cycle
                        break;
                    case BehaviorState.Success:
                        continue;
                    case BehaviorState.Running:
                        childRunning = true;
                        this.RunningChildren.Add(behavior);
                        continue;
                    default:
                        Debug.LogError("Not a valid BehaviorState.");
                        break;
                }
                FinishedRunningChildren.Clear();
            }
        }
    }
}
