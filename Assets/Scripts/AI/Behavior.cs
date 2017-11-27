using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
    public class Behavior : ScriptableObject
    {
        public string BehaviorName = "New Behavior";
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

        public virtual IEnumerator Tick()
        {
            yield return null;
        }
    }
}