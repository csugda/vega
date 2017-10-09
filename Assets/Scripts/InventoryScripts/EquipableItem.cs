using UnityEngine;
using System;
namespace Assets.Scripts.InventoryScripts
{
    [Serializable]
    class EquipableItem : PickupItem
    {
        public GameObject EquipmentPrefab;
        public EquipmentSlots location;
        public override void OnItemUsed()
        {
            InventoryGO.GetComponent<Inventory>().AddItem(
                InventoryGO.GetComponent<EquippedGearManager>().Equip(this, location));
        }
        public virtual void OnEquipped()
        {
            throw new NotImplementedException();
        }
        public virtual void OnUnequipped()
        {
            throw new NotImplementedException();
        }
    }
}
