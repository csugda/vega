using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    [Serializable]
    public class WeaponItem : PickupItem
    {
        public int Damage;

        public override void OnItemUsed()
        {
            base.OnItemUsed();
        }
    }
}
