using UnityEngine;
namespace Assets.Scripts.InventoryScripts.Items
{
    class ArmorItem : EquipableItem
    {
        public int armorValue;

        public override void OnEquipped()
        {
            HealthManager.onMaxHealthChanged.Invoke(armorValue);
            //this is where we would but the things to change the player model when equiping stuff
        }

        public override void OnUnequipped()
        {
            HealthManager.onMaxHealthChanged.Invoke(-armorValue);
        }


        public override void OnItemUsed()
        {
            base.OnItemUsed();//equip item
        }
        
    }
}
