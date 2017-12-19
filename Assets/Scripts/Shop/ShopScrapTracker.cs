using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShopScrapTracker : MonoBehaviour {

    public int scrapCount;
	void Start ()
    {
        ReadScrap();
        SetText();
	}

    private void OnDestroy()
    {
        //sometimes when closing the game from the shop screen the carry is destroyed first, avoiding nulls
        if (GameObject.Find("SHOP_ITEM_CARRYOVER"))
        {
            ItemCarryover carry = GameObject.Find("SHOP_ITEM_CARRYOVER").GetComponent<ItemCarryover>();
            carry.scrapAmmount = scrapCount;
        }
    }

    public Text scrapText;
    private void SetText()
    {
        scrapText.text = scrapCount + "";
    }

    private void ReadScrap()
    {
        try
        {
            string line;
            StreamReader reader = new StreamReader("Assets/Resources/Scrap.txt");
            using (reader)
            {
                line = reader.ReadLine();
                if (line == null)
                    Debug.LogError("No scrap info saved at 'Assets/Resources/Scrap.txt'");
                scrapCount = int.Parse(line);
            }
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message);
            return;
        }
    }

    public void ChangeScrap(int ammouunt)
    {
        scrapCount += ammouunt;
        SetText();
    }
}
