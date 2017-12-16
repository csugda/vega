using Assets.Scripts.AI.TreeModel;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.AI
{
    [System.Serializable]
    public class BehaviorTreeElement : TreeElement
    {
        public string BehaviorName = "New Behavior";
        public BehaviorManager BehaviorTreeManager;

        private BehaviorState _CurrentState;

        public BehaviorTreeElement(string name, int depth, int id) 
            : base(name, depth, id)
        {
            this.BehaviorName = name;
        }

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