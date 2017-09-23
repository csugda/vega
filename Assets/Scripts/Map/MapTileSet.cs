using System;
using UnityEngine;

namespace Assets.Scripts.Map
{
    [Serializable]
    public class MapTileSet : MonoBehaviour
    {
        public MapTile[] FloorTiles;
        public MapTile[] InnerWallTiles;
        public MapTile[] OuterWallTiles;

        public GameObject GetTileOfType(TileType type)
        {
            switch (type)
            {
                case TileType.Floor:
                    return WeightedRandom(FloorTiles);
                case TileType.InnerWall:
                    return WeightedRandom(InnerWallTiles);
                case TileType.OuterWall:
                    return WeightedRandom(OuterWallTiles);
                default:
                    Debug.LogError("Unable to find correct tile for type " + type.ToString());
                    return null;
            }
        }

        private GameObject WeightedRandom(MapTile[] Tiles)
        {
            int TotalWeight = 0;
            foreach (MapTile t in Tiles)
            {
                TotalWeight += t.Weight;
            }
            int rand = RandIndex(TotalWeight);
            int w = 0;
            foreach (MapTile t in Tiles)
            {
                w += t.Weight;
                if (w > rand)
                    return t.Tile;
            }
            //if this happens something went wrong, let me know. -Miles
            Debug.Log("MapTileSet.WeightedRandom failed to choose a tile, defaulting to null");
            return null;
        }

        private int RandIndex(int max)
        {
            return UnityEngine.Random.Range(0, max);
        }
    }
}
