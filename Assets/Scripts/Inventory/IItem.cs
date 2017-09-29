using System;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

namespace Assets.Scripts.Inventory
{
    interface IItem
    {
        string Name();
        Sprite Image();
        ItemType Type();
        
    }
}
