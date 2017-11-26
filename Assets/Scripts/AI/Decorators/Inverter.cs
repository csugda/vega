using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.AI.Decorators
{
    public class Inverter : BehaviorDecorator
    {
        public override IEnumerator Tick()
        {
            this.BehaviorTreeManager.StartCoroutine(DecoratedBehavior.Tick());

            switch (DecoratedBehavior.CurrentState)
            {
                case BehaviorState.Fail:
                    this.CurrentState = BehaviorState.Success;
                    break;
                case BehaviorState.Success:
                    CurrentState = BehaviorState.Fail;
                    break;
                case BehaviorState.Running:
                    this.CurrentState = BehaviorState.Running;
                    break;
                default:
                    Debug.LogError("Something went wrong in an inverter.");
                    break;
            }
            yield return null;
        }
    }

}