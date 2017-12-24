
using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    class EmptySlot : IInventoryItem
    {
        public int StackSize { get; set; }
        public Sprite Image { get; set; }
        public string Name { get; set; }
        public String ItemInfo { get; set; }
        public int ItemPrice { get; set; }
        public EmptySlot()
        {
            this.Name = "Empty";
            this.StackSize = 1;
            this.Image = null;
        }

        public void OnItemUsed()
        { }

        public bool Equals(IInventoryItem other)
        {
            return this.Name == other.Name;
        }
    }
}
