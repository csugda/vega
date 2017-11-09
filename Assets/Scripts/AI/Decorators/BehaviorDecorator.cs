using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public abstract class BehaviorDecorator : IBehavior
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

    public IEnumerator Tick()
    {
        yield return null;
    }
}
