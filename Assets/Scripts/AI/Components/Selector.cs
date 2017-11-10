using System;
using System.Collections;
using System.Collections.Generic;

public class Selector : BehaviorComponent
{
    public override IEnumerator Tick()
    {
        foreach(var behavior in RunningChildren)
        {
            StartCoroutine(behavior.Tick());
        }

        foreach(var behavior in SubBehaviors)
        {
            //If behavior is still in running state here, it already had its turn.
            if (behavior.CurrentState == BehaviorState.Running) continue;
            //if the behavior is NOT in Running right now, it has finished or has 
            //not started yet. Give it some sugah.
            StartCoroutine(behavior.Tick());
            switch(behavior.CurrentState)
            {
                case BehaviorState.Success:
                    //we have our selection
                    CurrentState = BehaviorState.Success;
                    break;
                case BehaviorState.Fail:
                    //keep going!
                    CurrentState = BehaviorState.Null;
                    break;
                case BehaviorState.Running:
                    CurrentState = BehaviorState.Running;
                    RunningChildren.Add(behavior);
                    break;
                default:
                    UnityEngine.Debug.LogError("Oh shiz, a selector broke BAD!");
                    break;
            }
        }
        yield return null;
    }
}