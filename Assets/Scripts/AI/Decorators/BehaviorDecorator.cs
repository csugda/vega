using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BehaviorDecorator : MonoBehaviour, IBehavior
{
    IBehavior DecoratedBehavior;

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
