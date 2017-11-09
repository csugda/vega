using System.Collections;
using UnityEngine;

public abstract class Behavior : MonoBehaviour
{
    private BehaviorState _CurrentState;
    public BehaviorState CurrentState
    {
        get
        {
            return _CurrentState;
        }
        protected set
        {
            _CurrentState = value;
        }
    }

    public abstract IEnumerator Tick();
}
