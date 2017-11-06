using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorComponent : MonoBehaviour, IBehavior
{
    LinkedList<IBehavior> SubBehaviors;

    public BehaviorState CurrentState
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public IEnumerator Tick()
    {
        foreach(var behavior in SubBehaviors)
        {
            yield return StartCoroutine(behavior.Tick());
        }
    }
}
