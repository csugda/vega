using UnityEngine;
namespace Assets.Scripts.InventoryScripts.Items
{
    class ArmorItem : EquipableItem
    {
        public int armorValue;

        public override void OnEquipped()
        {
            if (ManagerGO == null)
                ManagerGO = GameObject.Find("ManagerGO");
            ManagerGO.GetComponent<HealthManager>().ChangeMaxHealth(armorValue);
            //this is where we would but the things to change the player model when equiping stuff
        }

        public override void OnUnequipped()
        {
            if (ManagerGO == null)
                ManagerGO = GameObject.Find("ManagerGO");
            ManagerGO.GetComponent<HealthManager>().ChangeMaxHealth(-armorValue);
        }


        public override void OnItemUsed()
        {
            base.OnItemUsed();//equip item
        }
        
    }
}
