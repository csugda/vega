using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.InventoryScripts.Items
{
    [Serializable]
    public class EnergyItem : PickupItem
    {
        public int energyAmmount;

        public override void OnItemUsed()
        {
            PlayerEnergy.ChangeEnergy.Invoke(energyAmmount);
        }
    }
}
