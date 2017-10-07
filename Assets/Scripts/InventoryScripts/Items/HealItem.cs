using System;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts
{
    [Serializable]
    public class HealItem : PickupItem
    {
        public int HealAmount;

        public override void OnItemUsed()
        {
            Debug.Log("healPlayer " + HealAmount + "!");
        }
    }
}