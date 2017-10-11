using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts.Items
{
    [Serializable]
    public class WeaponItem : EquipableItem
    {
        //my first guess for how to implement this is to instantiate a gameobject that already has the weapon 
        //scripts on it directly childed to the player gameobject


        public override void OnEquipped()
        {
            base.OnEquipped();
            
        }
        public override void OnUnequipped()
        {
            base.OnUnequipped();
        }
        public override void OnItemUsed()
        {
            base.OnItemUsed();
        }
    }
}
