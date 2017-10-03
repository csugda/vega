using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{
    public CollectibleManager ui;

    // Use this for initialization
    void Start()
    {
        ui = GameObject.FindWithTag("CollectibleManager").GetComponent<CollectibleManager>();
        // finds a tagged GO on the heirarchy and accesses its component script

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider other) // triggers on contact with a collider
    {
        if (other.gameObject.CompareTag("Player")) // compares the tag of an object
        {
            ui.incrementScrapCoin(); // access method from CollectibleManager script
            this.gameObject.SetActive(false); // sets gameobject to inactive
        }

    }
}