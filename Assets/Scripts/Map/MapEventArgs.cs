using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Map
{
    public class MapEventArgs : EventArgs
    {
        public TileType Type { get; set; }

        public MapEventArgs(TileType type)
        {
            Type = type;
        }
    }
}
