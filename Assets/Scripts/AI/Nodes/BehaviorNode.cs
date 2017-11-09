using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorNode : IBehavior
{
    private BehaviorState _CurrentState;
    public BehaviorState CurrentState
    {
        get
        {
            return _CurrentState;
        }
    }

    public IEnumerator Tick()
    { yield return null; }
}
