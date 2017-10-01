using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    public class Inventory : MonoBehaviour
    {
        private class InventorySlot
        {
            public Item item;
            public int count;
            public InventorySlot (Item i, int c)
            {
                item = i; count = c;
            }
        }


        private void Start()
        {
            inventory = new InventorySlot[invSize];
            for (int i = 0; i < invSize; ++i)
            {
                inventory[i] = new InventorySlot(new EmptySlot(), 1);
            }
            this.AddItem(new HealItem("welder", 1, null, 20));
        }

        public int invSize;
        private InventorySlot[] inventory;

        public Item GetItem(int i)
        {
            if (i < 0 || i >= inventory.Length)
               throw new System.Exception("Index " + i + " out of range. 0 <= i < " + inventory.Length);
            else
                return inventory[i].item;
        }

        public void UseItem(int v)
        {
            inventory[v].item.OnItemUsed();
            inventory[v].count = (inventory[v].item is EmptySlot) ? inventory[v].count : inventory[v].count - 1;
            if (inventory[v].count == 0)
                inventory[v] = new InventorySlot(new EmptySlot(), 1);
        }
       
        public bool AddItem(Item item)
        {
            for (int i = 0; i < invSize; ++i)
            {
                if (inventory[i].item.Equals(item))
                {
                    if (inventory[i].count >= inventory[i].item.stacksize)
                        return false;
                    else
                    { 
                        inventory[i].count += 1;
                        return true;
                    }
                }
                if (inventory[i].item is EmptySlot)
                {
                    inventory[i] = new InventorySlot(item, 1);
                    return true;
                }
            }
            return false;
        }

    }
}