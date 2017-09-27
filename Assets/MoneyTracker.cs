using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneyTracker : MonoBehaviour {

    // declare variables for use in counting collectibles
    public Text scrapText;
    private int scrapAmount;
    // Use this for initialization
    void Start () {
        // define variables for counting collectibles
        scrapAmount = 0;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public bool ChangeScrap(int amount)
    {
        scrapAmount += amount;
        SetScrapText();
        return true;
    }

    // method for setting the amount of scrap collected
    void SetScrapText()
    {
        scrapText.text = "Piles of Scrap: " + scrapAmount.ToString();
    }
}
