using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map.Map_Tiles
{
    [Serializable]
    public class MapTileSet : MonoBehaviour
    {
        public MapTile[] MapTiles;

        public MapTile GetTileOfType(TileType type, MapRandom rand)
        {
            return WeightedRandom(type, rand);
        }

        private MapTile WeightedRandom(TileType type, MapRandom randGen)
        {
            var typeQuery =
                from tile in MapTiles
                where tile.Type == type
                select tile;

            int TotalWeight = 0;
            foreach (var tile in typeQuery)
            {
                TotalWeight += tile.Weight;
            }

            //UnityEngine.Random meow = new UnityEngine.Random();
            int rand = randGen.GetInt(0, TotalWeight);
            int w = 0;
            foreach (MapTile t in typeQuery)
            {
                w += t.Weight;
                if (w > rand)
                    return t;
            }
            //if this happens something went wrong, let me know. -Miles (and also Josh)
            Debug.LogError("MapTileSet.WeightedRandom failed to choose a tile, defaulting to null");
            return null; //TODO: Make an ErrorTile? (Probably)
        }
    }
}
