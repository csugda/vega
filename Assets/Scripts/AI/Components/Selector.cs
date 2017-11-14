﻿using System;
using System.Collections;
using System.Collections.Generic;

public class Selector : BehaviorComponent
{
    public override IEnumerator Tick()
    {
        foreach (var behaviorRun in RunningChildren)
        {
            StartCoroutine(behaviorRun.Tick());
            if (behaviorRun.CurrentState != BehaviorState.Running)
            {
                FinishedRunningChildren.Add(behaviorRun);
            }
        }

        foreach (var behavior in SubBehaviors)
        {
            if (behavior.CurrentState == BehaviorState.Running ||
                FinishedRunningChildren.Contains(behavior)) continue;
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
        RunningChildren.RemoveWhere(a => FinishedRunningChildren.Contains(a));

        yield return null;
    }
}