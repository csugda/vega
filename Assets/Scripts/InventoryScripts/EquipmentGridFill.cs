using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.Scripts.InventoryScripts;
using UnityEngine.UI;

public class EquipmentGridFill : MonoBehaviour {
    public GameObject buttonPrefab;
    public GameObject inventoryGO;
    private Inventory inventory;
	// Use this for initialization
	void Awake () {
        inventory = inventoryGO.GetComponent<Inventory>();
        
	}
	
	public void RedrawGrid()
    {
        foreach (Transform ch in this.transform)
            Destroy(ch.gameObject);
        if (inventory == null)
            this.Awake();
        for (int i = 0; i < inventory.invSize; ++i)
        {
            GameObject button = Instantiate(buttonPrefab, this.transform);

            if (inventory.GetItem(i).Image != null)
            {
                //button.GetComponent<SpriteRenderer>().sprite = inventory.GetItem(i).Image;
            }

            button.transform.Find("NameText").gameObject.GetComponent<Text>().text = 
                inventory.GetItem(i).Name;

            button.transform.Find("CountText").gameObject.GetComponent<Text>().text = 
                inventory.GetItemCount(i) == 1 ? "" : "x" + inventory.GetItemCount(i);

            button.transform.name = "" + i;

            button.transform.Find("InfoPanel").GetComponentInChildren<Text>().text =
                inventory.GetItem(i).ItemInfo;

            button.GetComponent<Button>().onClick.AddListener
                (() => inventory.UseItem(int.Parse(button.transform.name)));

            button.GetComponent<Button>().onClick.AddListener
                (() => RedrawGrid());
        }
    }
}

