using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.InventoryScripts.Items
{
    class HealthUpItem : PickupItem
    {
        public int ammount;

        public override void OnItemUsed()
        {
            HealthManager.onMaxHealthChanged.Invoke(ammount);
        }
    }
}
