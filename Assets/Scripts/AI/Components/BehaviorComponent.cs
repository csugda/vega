using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI.Components
{
    public abstract class BehaviorComponent : BehaviorTreeElement
    {
        protected LinkedList<BehaviorTreeElement> SubBehaviors;
        protected HashSet<BehaviorTreeElement> RunningChildren;
        protected HashSet<BehaviorTreeElement> FinishedRunningChildren;

        public BehaviorComponent(string name, int depth, int id) 
            : base(name, depth, id)
        {
        }
    }
}
