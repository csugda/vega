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
		for (int i = 0; i < inventory.invSize; ++i)
        {
            GameObject button = Instantiate(buttonPrefab, this.transform);
            if (inventory.GetItem(i).image != null)
            { button.GetComponent<SpriteRenderer>().sprite = inventory.GetItem(i).image; }
            button.GetComponentInChildren<Text>().text = inventory.GetItem(i).name;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

