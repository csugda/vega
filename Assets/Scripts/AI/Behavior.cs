using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
    public abstract class Behavior : ScriptableObject
    {
        public BehaviorManager BehaviorTreeManager;

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
}