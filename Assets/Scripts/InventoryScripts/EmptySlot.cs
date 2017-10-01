using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine.UI;

namespace Assets.Scripts.InventoryScripts
{
    class EmptySlot : Item
    {
        public EmptySlot()
        {
            this.name = "Empty";
            this.stacksize = 1;
            this.image = null;
        }

        public override void OnItemUsed()
        {
            
        }
    }
}
