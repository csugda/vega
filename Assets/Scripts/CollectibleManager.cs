using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CollectibleManager : MonoBehaviour {

    public int scrapCount = 0;
    public Text scrapText;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
   public void incrementScrapCoin ()
    {
        scrapCount += 1;
        scrapText.text = "Piles of Scrap: " + scrapCount;
    }
    public void incrementScrapBox()
    {
        scrapCount += 2;
        scrapText.text = "Piles of Scrap: " + scrapCount;
    }
}
