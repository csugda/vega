﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class BehaviorDecorator : Behavior
{
    Behavior DecoratedBehavior;
}
