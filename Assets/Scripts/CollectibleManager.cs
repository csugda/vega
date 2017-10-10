using UnityEngine.UI;
using UnityEngine;

public class CollectibleManager : MonoBehaviour
{
    public int scrapCount = 0;
    public Text scrapText;

    void Start()
    {
        IncrementScrap(0);
    }
    public void IncrementScrap(int ammount)
    {
        scrapCount += ammount;
        scrapText.text = "Piles of Scrap: " + scrapCount;
    }

}
