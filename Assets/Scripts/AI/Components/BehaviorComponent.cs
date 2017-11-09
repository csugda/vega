using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorComponent : Behavior
{
    protected LinkedList<Behavior> SubBehaviors;
    protected HashSet<Behavior> RunningChildren;
}
