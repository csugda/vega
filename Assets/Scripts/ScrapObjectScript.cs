using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapObjectScript : MonoBehaviour
{
    private CollectibleManager ui;
    public int scrapValue;
    // Use this for initialization
    void Start()
    {
        ui = GameObject.Find("ManagerGO").GetComponent<CollectibleManager>();
        // finds a tagged GO on the heirarchy and accesses its component script
    }

    void OnTriggerEnter(Collider other) // triggers on contact with a collider
    {
        if (other.gameObject.CompareTag("Player")) // compares the tag of an object
        {
            ui.IncrementScrap(scrapValue); // access method from CollectibleManager script
            Destroy(this.gameObject); // sets gameobject to inactive
        }

    }
}