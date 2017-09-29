using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentGridFill : MonoBehaviour {
    public GameObject emptySlotPrefab;
    public int numberOfSlots;
	// Use this for initialization
	void Start () {
		for (int i = 0; i < numberOfSlots; ++i)
        {
            Instantiate(emptySlotPrefab, this.transform);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
