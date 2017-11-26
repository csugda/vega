using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI.Components
{
    public abstract class BehaviorComponent : Behavior
    {
        protected LinkedList<Behavior> SubBehaviors;
        protected HashSet<Behavior> RunningChildren;
        protected HashSet<Behavior> FinishedRunningChildren;
    }
}
