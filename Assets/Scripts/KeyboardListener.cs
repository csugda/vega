using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class InventoryKeyEvent : UnityEvent<string>
{ }


public class KeyboardListener : MonoBehaviour {
    public string inventoryOpenKey;
    public GameObject Menu;
    public InventoryKeyEvent invKeyEvent;

	
	void Update () {
        if (Input.GetKeyDown(inventoryOpenKey))
        {
            if(!Menu.activeSelf)
            {
                Menu.SetActive(true);
                invKeyEvent.Invoke(inventoryOpenKey);
            }
            else
            {
                Menu.SetActive(false);
            }
            
        }
	}

}
