using System;
using UnityEngine;

namespace Assets.Scripts.InventoryScripts
{
    public interface InventoryItem : IEquatable<InventoryItem>
    {
        string Name { get; set; }
        int StackSize { get; set; }
        Sprite Image { get; set; }
        String ItemInfo { get; set; }

        void OnItemUsed();
    }
}
