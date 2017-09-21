using System;
using UnityEngine;

namespace Assets.Scripts.Map
{
    [Serializable]
    public class MapParameters
    {
        public int Height;
        public int Width;

        public Int64 Seed;

        //The prefabs to choose from when creating the map
        public GameObject[] FloorTiles;
        public GameObject[] WallTiles;
        public GameObject[] OuterWallTiles;
    }
}


