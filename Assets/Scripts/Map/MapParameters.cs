using System;
using UnityEngine;

namespace Assets.Scripts.Map
{
    [Serializable]
    public class MapParameters
    {
        public int Height;
        public int Width;

        public bool GenerateRandomMap;
        public Int64 Seed;
        
        public int MapSectors;
        public Vector3 MinimumRoomSize;
    }
}


