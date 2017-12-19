using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;
using System.IO;
using System;

public class ScrapEvent : UnityEvent<int> { }
public class CollectibleManager : MonoBehaviour
{
    public static ScrapEvent onScrapChanged = new ScrapEvent();

    public int scrapCount;
    public Text scrapText;

    void Start()
    {
        ReadCarryScrap();
        onScrapChanged.AddListener(IncrementScrap);
        IncrementScrap(0);
    }

    private void ReadCarryScrap()
    {
        //read scrap carried from shop, if shop carry is present
        if (GameObject.Find("SHOP_ITEM_CARRYOVER"))
        {
            ItemCarryover carry = GameObject.Find("SHOP_ITEM_CARRYOVER").GetComponent<ItemCarryover>();
            scrapCount = carry.scrapAmmount;
            carry.Finished();
        }
        else
        {
            Debug.LogWarning("SHOP_ITEM_CARRYOVER not present in scene, most likely game was not launched from shop scene. " +
                    "\nUsing 0 scrap");
            scrapCount = 0;
        }
    }

    private void OnDestroy()
    {
        //save scrap ammount
        string path = "Assets/Resources/Scrap.txt";

        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(scrapCount);
        writer.Close();
    }

    public void IncrementScrap(int ammount)
    {
        scrapCount += ammount;
        scrapText.text = "" + scrapCount;
    }

}
