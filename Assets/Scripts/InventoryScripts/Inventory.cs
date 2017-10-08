using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    public class Inventory : MonoBehaviour
    {
        private class InventorySlot
        {
            public IInventoryItem item;
            public int count;
            public InventorySlot (IInventoryItem i, int c)
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

        public IInventoryItem GetItem(int i)
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

        public bool AddItem(IInventoryItem item)
        {
            for (int i = 0; i < invSize; ++i)
            {
                if (inventory[i].item.Equals(item))
                {
                    if (inventory[i].count >= inventory[i].item.StackSize)
                    {
                        Debug.Log("stack size limit met");
                        return false;
                    }
                    else
                    {
                        inventory[i].count += 1;
                        gridGen.RedrawGrid();
                        return true;
                    }
                }
                if (inventory[i].item is EmptySlot)
                {
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