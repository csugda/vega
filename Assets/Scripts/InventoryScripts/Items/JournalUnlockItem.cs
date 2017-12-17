using Assets.Scripts.Menu.Journal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts.Items
{
    [Serializable]
    class JournalUnlockItem : PickupItem
    {
        public int area;
        private JournalManager manager;
        public new void Start()
        {
            //I dont think that this makes a difference as OnItemUsed only gets called once, but it is the way to do things.
            manager = GameObject.Find("ManagerGO").GetComponent<JournalManager>();
        }
        
        
        public override void OnItemUsed()
        {
            manager.UnlockNextJournalEntry(area);
        }
    }
}
