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
	void Start () {
        inventory = inventoryGO.GetComponent<Inventory>();
        RedrawGrid();
	}
	
	void RedrawGrid()
    {
        foreach (Transform ch in this.transform)
            Destroy(ch.gameObject);
        for (int i = 0; i < inventory.invSize; ++i)
        {
            GameObject button = Instantiate(buttonPrefab, this.transform);

            if (inventory.GetItem(i).image != null)
            {
                button.GetComponent<SpriteRenderer>().sprite = inventory.GetItem(i).image;
            }

            button.transform.Find("NameText").gameObject.GetComponent<Text>().text = 
                inventory.GetItem(i).name;

            button.transform.Find("CountText").gameObject.GetComponent<Text>().text = 
                inventory.GetItemCount(i) == 1 ? "" : "x" + inventory.GetItemCount(i);

            button.transform.name = "" + i;

            button.GetComponent<Button>().onClick.AddListener
                (() => inventory.UseItem(int.Parse(button.transform.name)));

            button.GetComponent<Button>().onClick.AddListener
                (() => RedrawGrid());
        }
    }
}

