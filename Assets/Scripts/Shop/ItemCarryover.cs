using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.InventoryScripts;

public class ItemCarryover : MonoBehaviour {

    public IInventoryItem[] items = new IInventoryItem[4];
    public int itemCount;
    public void Start()
    {
        itemCount = 0;
        DontDestroyOnLoad(this.gameObject);
    }
    public void OnLevelWasLoaded(int level)
    {
        
    }
    public void AddItem(IInventoryItem item)
    {
        items[itemCount] = item;
        ++itemCount;
    }
}
