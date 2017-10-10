using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Events;

public class ScrapEvent : UnityEvent<int> { }
public class CollectibleManager : MonoBehaviour
{
    public static ScrapEvent onScrapChanged = new ScrapEvent();
    public int scrapCount = 0;
    public Text scrapText;

    void Start()
    {
        onScrapChanged.AddListener(IncrementScrap);
        IncrementScrap(0);
    }
    public void IncrementScrap(int ammount)
    {
        scrapCount += ammount;
        scrapText.text = "Piles of Scrap: " + scrapCount;
    }

}
