using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Inventory
{
    class HealItem : Item
    {
        public int healAmmount;
        
        public override int OnItemUse()
        {
            return healAmmount;
        }
    }
}
