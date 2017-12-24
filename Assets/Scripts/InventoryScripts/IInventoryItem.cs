using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    public interface IInventoryItem : IEquatable<IInventoryItem>
    {
        string Name { get; set; }
        int StackSize { get; set; }
        Sprite Image { get; set; }
        String ItemInfo { get; set; }
        int ItemPrice { get; set; }

        void OnItemUsed();
    }
}
