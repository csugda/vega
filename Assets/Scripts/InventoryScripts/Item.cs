using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts
{
    public abstract class Item
    {
        public string name;
        public int stacksize;
        public Sprite image;

        public abstract void OnItemUsed();
    }
}
