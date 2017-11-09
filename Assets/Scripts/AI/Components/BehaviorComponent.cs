using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorComponent : MonoBehaviour, IBehavior
{
    LinkedList<IBehavior> SubBehaviors;
    LinkedList<IBehavior> RunningChildren;

    private BehaviorState _CurrentState;
    public BehaviorState CurrentState
    {
        get
        {
            return _CurrentState;
        }
    }

    public abstract IEnumerator Tick();
}
