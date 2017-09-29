using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class ItemUseEvent : UnityEvent<string> { }


namespace Assets.Scripts.Inventory
{
    abstract class Item
    {
        public string name;
        public Sprite image;
        public int stackSize;
        public int quantity;
        public ItemUseEvent onUsedEvent;


        public virtual int OnItemUse()
        {
            onUsedEvent.Invoke(this.name);
            return 0;
        }

    }
}
