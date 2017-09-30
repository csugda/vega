using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts
{
    public class PickupItem : MonoBehaviour
    {
        public GameObject inventoryGO;
        public Item item;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                inventoryGO.GetComponent<Inventory>().AddItem(item);
                Destroy(this.gameObject);
            }
        }
    }
}
