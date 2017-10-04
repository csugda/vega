using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    public class Inventory : MonoBehaviour
    {
        private class InventorySlot
        {
            public InventoryItem item;
            public int count;
            public InventorySlot (InventoryItem i, int c)
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

            gridGen = EquipmentGrid.GetComponent<EquipmentGridFill>();
            gridGen.RedrawGrid();
        }

        public int invSize;
        private InventorySlot[] inventory;
        public GameObject EquipmentGrid;
        private EquipmentGridFill gridGen;

        public InventoryItem GetItem(int i)
        {
            if (i < 0 || i >= inventory.Length)
               throw new System.Exception("Index " + i + " out of range. 0 <= i < " + inventory.Length);
            else
                return inventory[i].item;
        }
        public int GetItemCount(int i)
        {
            if (i < 0 || i >= inventory.Length)
                throw new System.Exception("Index " + i + " out of range. 0 <= i < " + inventory.Length);
            else
                return inventory[i].count;
        }

        public void UseItem(int v)
        {
            inventory[v].item.OnItemUsed();
            inventory[v].count = (inventory[v].item is EmptySlot) ? inventory[v].count : inventory[v].count - 1;
            if (inventory[v].count == 0)
            {
                inventory[v] = new InventorySlot(new EmptySlot(), 1);
                ShuffleInventory(v);
                gridGen.RedrawGrid();
            }

        }

        private void ShuffleInventory(int p)
        {
            for (int i = p; i < invSize-1; ++i)
            {
                inventory[i] = inventory[i + 1];
                if (inventory[i].item is EmptySlot)
                    return;
            }
        }

        public bool AddItem(InventoryItem item)
        {
            Debug.Log("add " + item.Name + " to inventory");
            for (int i = 0; i < invSize; ++i)
            {
                if (inventory[i].item.Equals(item))
                {
                    Debug.Log("found a match");
                    if (inventory[i].count >= inventory[i].item.StackSize)
                    {
                        Debug.Log("stack size limit met");
                        return false;
                    }
                    else
                    {
                        Debug.Log("increase stack to " + (inventory[i].count + 1));
                        inventory[i].count += 1;
                        gridGen.RedrawGrid();
                        return true;
                    }
                }
                if (inventory[i].item is EmptySlot)
                {
                    Debug.Log("no match, filling new slot");
                    inventory[i] = new InventorySlot(item, 1);
                    gridGen.RedrawGrid();
                    return true;
                }
            }
            Debug.Log("inventory full");
            return false;
        }
    }
}