
using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    class EmptySlot : InventoryItem
    {
        public int StackSize { get; set; }
        public Sprite Image { get; set; }
        public string Name { get; set; }
        public String ItemInfo { get; set; }

        public EmptySlot()
        {
            this.Name = "Empty";
            this.StackSize = 1;
            this.Image = null;
        }

        public void OnItemUsed()
        { }

        public bool Equals(InventoryItem other)
        {
            return this.Name == other.Name;
        }
    }
}
