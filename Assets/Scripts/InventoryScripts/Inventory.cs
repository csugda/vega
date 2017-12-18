using System;
using UnityEngine;
using UnityEngine.Events;
namespace Assets.Scripts.InventoryScripts
{
    public class InventoryChangeEvent : UnityEvent { }
    public class Inventory : MonoBehaviour
    {
        public static InventoryChangeEvent onInventoryChanged = new InventoryChangeEvent();
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
            ReadCarryItems();
            Inventory.onInventoryChanged.Invoke();
        }

        private void ReadCarryItems()
        {
            if (GameObject.Find("SHOP_ITEM_CARRYOVER"))
            {
                ItemCarryover carry = GameObject.Find("SHOP_ITEM_CARRYOVER").GetComponent<ItemCarryover>();
                for (int i = 0; i < carry.itemCount; ++i)
                {
                    //Debug.Log("Adding " + carry.items[i].Name + " to inventory");
                    this.AddItem(carry.items[i]);
                }
                carry.Finished();
            }
            else
                Debug.LogWarning("SHOP_ITEM_CARRYOVER not present in scene, most likely game was not launched from shop scene. " +
                    "\nLoading default inventory");
        }

        public int invSize;
        private InventorySlot[] inventory;

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
            IInventoryItem item = inventory[v].item;
            inventory[v].count = (inventory[v].item is EmptySlot) ? inventory[v].count : inventory[v].count - 1;
            if (inventory[v].count == 0)
            {
                inventory[v] = new InventorySlot(new EmptySlot(), 1);
                ShuffleInventory(v);
                Inventory.onInventoryChanged.Invoke();
            }
            item.OnItemUsed(); //at the end so that if using an item would add an item it wont overfill the inventory
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
                        continue;
                    }
                    else
                    {
                        inventory[i].count += 1;
                        Inventory.onInventoryChanged.Invoke();
                        return true;
                    }
                }
                if (inventory[i].item is EmptySlot)
                {
                    inventory[i] = new InventorySlot(item, 1);
                    Inventory.onInventoryChanged.Invoke();
                    return true;
                }
            }
            Debug.Log("inventory full");
            return false;
        }
    }
}