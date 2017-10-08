using System;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts
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