using System;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts
{
    [Serializable]
    public class HealItem : PickupItem
    {
        public int healAmount;
        private HealthManager manager;
        public void Start()
        {
            manager = GameObject.Find("ManagerGO").GetComponent<HealthManager>();
        }
        public override void OnItemUsed()
        {
            manager.ChangeHealth(healAmount);
        }
    }
}