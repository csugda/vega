using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrapValueCoin : MonoBehaviour {
    // declare variables for use in counting collectibles
      private Text scrapText;
      private int scrapAmount;

    // Use this for initialization
    void Start () {
        scrapText = GameObject.Find("CollectibleCanvas").GetComponentInChildren<Text>();
        // define variables for counting collectibles
        scrapAmount = 0;
        SetScrapText();

    }

    void OnTriggerEnter(Collider other) // triggers on contact with a collider
    {
        if (other.gameObject.CompareTag("Player")) // compares the tag of an object
        {
            this.gameObject.SetActive(false); // sets gameobject to inactive
              scrapAmount++; // increments the scrap count
              SetScrapText(); // calls method for setting the amount of scrap collected
        }
    }
    // method for setting the amount of scrap collected
    void SetScrapText()
    {
        scrapText.text = "Piles of Scrap: " + scrapAmount.ToString();
    }
    // Update is called once per frame
    void Update () {
		
	}
}
