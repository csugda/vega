using System;
using UnityEngine;

namespace Assets.Scripts.Map
{
    [Serializable]
    public class MapParameters
    {
        public Vector3 MapBounds;

        public bool GenerateRandomMap;
        public int Seed;
        
        public int MapSectors;
        public Vector3 MinimumRoomSize;
        public Vector3 MaximumRoomSize;
    }
}


