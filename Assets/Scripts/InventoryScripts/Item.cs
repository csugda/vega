using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts
{
    public abstract class Item : IEquatable<Item>
    {
        public string name;
        public int stacksize;
        public Sprite image;

        public abstract void OnItemUsed();

        public int CompareTo(object obj)
        {
            if (obj == null) return 1;

            Item otherItem = obj as Item;
            if (otherItem != null)
                return this.name.CompareTo(otherItem.name);
            else
                throw new ArgumentException("Object is not a Item");
        }

        public bool Equals(Item other)
        {
            return this.name == other.name;
        }
    }
}
