using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapObjectScript : MonoBehaviour
{
    public int scrapValue;
    // Use this for initialization
    

    void OnTriggerEnter(Collider other) // triggers on contact with a collider
    {
        if (other.gameObject.CompareTag("Player")) // compares the tag of an object
        {
            CollectibleManager.onScrapChanged.Invoke(scrapValue); // access method from CollectibleManager script
            Destroy(this.gameObject); // sets gameobject to inactive
        }

    }
}