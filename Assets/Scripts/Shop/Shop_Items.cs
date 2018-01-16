using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.InventoryScripts;
using UnityEngine.UI;

public class Shop_Items : MonoBehaviour
{
    public PickupItem[] availableItems;
    public GameObject ItemPrefab;
    public int NumberOfItemsToShow;
    public GameObject infoParent;

    private ShopScrapTracker money;
    private void Start()
    {
        money = GameObject.Find("Scrap").GetComponent<ShopScrapTracker>();

        NumberOfItemsToShow = NumberOfItemsToShow < 1 ? 1 : NumberOfItemsToShow > 6 ? 6 : NumberOfItemsToShow;
        for (int i = 0; i < NumberOfItemsToShow; ++i)
        {
            PickupItem thisItem = availableItems[UnityEngine.Random.Range(0, availableItems.Length)];

            GameObject button = Instantiate(ItemPrefab, this.transform);

            if (thisItem.Image != null)
            {
                button.GetComponent<Image>().sprite = thisItem.Image;
                button.transform.Find("NameText").gameObject.GetComponent<Text>().text = "";
            }
            else
            {
                button.transform.Find("NameText").gameObject.GetComponent<Text>().text =
                thisItem.Name;
            }

            

            button.transform.Find("PriceText").gameObject.GetComponent<Text>().text =
                "" + thisItem.ItemPrice;

            button.transform.name = "Item " + i;

            button.transform.Find("InfoPanel").GetComponentInChildren<Text>().text =
                thisItem.ItemInfo;

            button.GetComponent<ShowItemInfo>().parentGO = infoParent;

            //button.GetComponent<Button>().onClick.AddListener
            //    (() => this.Buy(thisItem));

            //deactivate the button once it is used
            button.GetComponent<Button>().onClick.AddListener
                (() =>
                {
                    if (this.Buy(thisItem))
                    {
                        //Color temp = button.GetComponent<Image>().color;
                        Color temp = Color.red;
                        button.GetComponent<Image>().color = temp;
                        button.GetComponent<Button>().onClick.RemoveAllListeners();
                    }
                }
                );
            button.GetComponent<RectTransform>().localScale.Set(2, 2, 2);
        }

    }
    public bool Buy(IInventoryItem i)
    {
        if (money.scrapCount > i.ItemPrice)
        {
            money.ChangeScrap(-i.ItemPrice);
            ItemCarryover carry = GameObject.Find("SHOP_ITEM_CARRYOVER").GetComponent<ItemCarryover>();
            carry.AddItem(i);
            return true;
        }
        return false;

    }
}
