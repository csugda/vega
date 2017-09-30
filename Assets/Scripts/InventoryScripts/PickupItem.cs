using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts
{
    public class PickupItem : MonoBehaviour
    {
        public enum ItemType { HealItem , Weapon };
        public ItemType itemTypeChoice;
        public GameObject inventoryGO;
        public HealItem healItem;
        public WeaponItem weaponItem;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                switch (itemTypeChoice)
                {
                    case ItemType.HealItem:
                        inventoryGO.GetComponent<Inventory>().AddItem(healItem);
                        break;
                    case ItemType.Weapon:
                        inventoryGO.GetComponent<Inventory>().AddItem(weaponItem);
                        break;
                }
                Destroy(this.gameObject);

            }
        }
    }
}
