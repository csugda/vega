using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.AI.Decorators
{
    public abstract class BehaviorDecorator : BehaviorTreeElement
    {
        protected BehaviorTreeElement DecoratedBehavior;

        public BehaviorDecorator(string name, int depth, int id) 
            : base(name, depth, id)
        {
        }
    } 
}
