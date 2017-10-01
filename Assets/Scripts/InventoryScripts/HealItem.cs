using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Assets.Scripts.InventoryScripts
{
    public class HealItem : Item
    {
        public int healAmmount;
        

        public HealItem(string _name, int _stackSize, Sprite _image, int _healAmmount)
        {
            this.name = _name;
            this.stacksize = _stackSize;
            this.image = _image;
            this.healAmmount = _healAmmount;
        }

        public override void OnItemUsed()
        {
            Debug.Log("healPlayer " + healAmmount + "!");
        }
    }
}