using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBehavior
{
    BehaviorState CurrentState { get; }

    IEnumerator Tick();
}
