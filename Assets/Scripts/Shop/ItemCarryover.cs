using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.InventoryScripts;

public class ItemCarryover : MonoBehaviour {
    //[0] = Health; [1] = speed; [2] = damage; [3] = fireRate
    public int[] upgradeLevels = new int[4];

    public IInventoryItem[] items = new IInventoryItem[4];
    public int itemCount;
    public void Start()
    {
        itemCount = 0;
        DontDestroyOnLoad(this.gameObject);
    }
    
    public void AddItem(IInventoryItem item)
    {
        items[itemCount] = item;
        ++itemCount;
    }
}
