using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    class EquippedGearManager : MonoBehaviour
    {

        public GameObject equipmentUI;

        public EquipableItem /*lArm, rArm, */head, torso, legs;
        private void Start()
        {
            head.OnEquipped();
        }
        public EquipableItem Equip(EquipableItem item, EquipmentSlots location)
        {
            EquipableItem oldItem = null;
            switch (location)
            {
                case EquipmentSlots.Head:
                    oldItem = head;
                    head = item;
                    break;
                case EquipmentSlots.Torso:
                    oldItem = torso;
                    torso = item;
                    break;
                case EquipmentSlots.Legs:
                    oldItem = legs;
                    legs = item;
                    break;
                //case EquipmentSlots.LArm:
                //    oldItem = lArm;
                //    lArm = item;
                //    break;
                //case EquipmentSlots.RArm:
                //    oldItem = rArm;
                //    rArm = item;
                //    break;
            }
            oldItem.OnUnequipped();
            item.OnEquipped();
            Inventory.onInventoryChanged.Invoke();
            return oldItem;
        }
    }
}
