using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    public class Inventory : MonoBehaviour
    {

        private void Start()
        {
            inventory = new Item[invSize];
            for (int i = 0; i < invSize; ++i)
            {
                inventory[i] = new EmptySlot();
            }
            inventory[0] = new HealItem("welder", 1, null, 20);
        }

        public int invSize;
        private Item[] inventory;

        public Item GetItem(int i)
        {
            if (i < 0 || i >= inventory.Length)
               throw new System.Exception("Index " + i + " out of range. 0 <= i < " + inventory.Length);
            else
                return inventory[i];
        }
    }
}