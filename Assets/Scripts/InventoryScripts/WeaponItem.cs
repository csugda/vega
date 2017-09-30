using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    [System.Serializable]
    public class WeaponItem : Item
    {
        public int dammage;


        public WeaponItem(string _name, int _stackSize, Sprite _image, int _dammage)
        {
            this.name = _name;
            this.stacksize = _stackSize;
            this.image = _image;
            this.dammage = _dammage;
        }

        public override void OnItemUsed()
        {
            throw new NotImplementedException();
        }
    }
}
