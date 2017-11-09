using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : BehaviorComponent
{
    
    public Sequencer(LinkedList<IBehavior> subBehaviors)
    {
        this.SubBehaviors = subBehaviors;
    }

    public override IEnumerator Tick()
    {
        bool childRunning = false;

        foreach (var behaviorRun in RunningChildren)
        {
            //StartCoroutine(behaviorRun.Tick());
        }

        foreach (var behavior in SubBehaviors)
        {
            if (behavior.CurrentState == BehaviorState.Running) continue;
            //StartCoroutine(behavior.Tick());
            switch (behavior.CurrentState)
            {
                case BehaviorState.Fail:
                    this._CurrentState = BehaviorState.Fail;
                    break;
                case BehaviorState.Success:
                    continue;
                case BehaviorState.Running:
                    childRunning = true;
                    this.RunningChildren.add(behavior);
                    continue;
                default:
                    Debug.LogError("Not a valid BehaviorState. Go perform sexual acts on a pig.");
            }
            
        }

        this._CurrentState = !childRunning ? BehaviorState.Success : BehaviorState.Running;
        yield return null;
    }
}
