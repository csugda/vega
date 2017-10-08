using System;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts.Items
{
    [Serializable]
    public class HealItem : PickupItem
    {
        public int healAmount;
        
        public override void OnItemUsed()
        {
            HealthManager.onHealthChanged.Invoke(healAmount);
        }
    }
}