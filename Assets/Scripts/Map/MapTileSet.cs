using System;
using UnityEngine;

namespace Assets.Scripts.Map
{
    [Serializable]
    public class MapTileSet : MonoBehaviour
    {
        public GameObject[] FloorTiles;
        public GameObject[] InnerWallTiles;
        public GameObject[] OuterWallTiles;

        public GameObject GetTileOfType(TileType type)
        {
            switch (type)
            {
                case TileType.Floor:
                    return FloorTiles[RandIndex(FloorTiles.Length)];
                case TileType.InnerWall:
                    return InnerWallTiles[RandIndex(InnerWallTiles.Length)];
                case TileType.OuterWall:
                    return OuterWallTiles[RandIndex(OuterWallTiles.Length)];
                default:
                    Debug.LogError("Unable to find correct tile for type " + type.ToString());
                    return null;
            }
        }
        private int RandIndex(int max)
        {
            return UnityEngine.Random.Range(0, max);
        }
    }
}
