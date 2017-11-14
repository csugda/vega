﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sequencer : BehaviorComponent
{
    public Sequencer(LinkedList<Behavior> subBehaviors)
    {
        this.SubBehaviors = subBehaviors;
    }

    public override IEnumerator Tick()
    {
        bool childRunning = false;

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
            StartCoroutine(behavior.Tick());
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
