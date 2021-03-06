﻿using UnityEngine;
using System;
namespace Assets.Scripts.InventoryScripts
{
    [Serializable]
    public class EquipableItem : PickupItem
    {
        public GameObject EquipmentPrefab;
        public EquipmentSlots location;
        public override void OnItemUsed()
        {
            ManagerGO.GetComponent<Inventory>().AddItem(
                ManagerGO.GetComponent<EquippedGearManager>().Equip(this, location));
        }
        public virtual void OnEquipped()
        {
            throw new NotImplementedException();
        }
        public virtual void OnUnequipped()
        {
            throw new NotImplementedException();
        }
        public override void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                if (ManagerGO.GetComponent<Inventory>().AddItem(this))
                    this.gameObject.SetActive(false);
            }
        }
    }
}
