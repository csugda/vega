using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.Inventory
{
    interface IStackableItem : IItem
    {
        int StackSize { get; set; }
        int Ammount { get; set; }
    }
}
