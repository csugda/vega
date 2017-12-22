using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.InventoryScripts;

public class ItemCarryover : MonoBehaviour {
    //[0] = Health; [1] = speed; [2] = damage; [3] = fireRate
    public int[] upgradeLevels = new int[4];

    public int scrapAmmount;

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
    private int done = 0;
    public int needToUse = 4; //there are currently 3 things that need this, it will delete itself when they are all done
                              // when something new needs it, increse this number.
    public void Finished()
    {
        ++done;
        if (done == needToUse)
            Destroy(this.gameObject);
    }
}
