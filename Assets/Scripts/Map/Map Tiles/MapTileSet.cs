﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map.Map_Tiles
{
    [Serializable]
    public class MapTileSet : MonoBehaviour
    {
        public MapTile[] MapTiles;

        public MapTile GetTileOfType(TileType type)
        {
            return WeightedRandom(type);
        }

        private MapTile WeightedRandom(TileType type)
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

            int rand = UnityEngine.Random.Range(0, TotalWeight);
            int w = 0;
            foreach (MapTile t in typeQuery)
            {
                w += t.Weight;
                if (w > rand)
                    return t;
            }
            //if this happens something went wrong, let me know. -Miles (and also Josh)
            Debug.LogError("MapTileSet.WeightedRandom failed to choose a tile, defaulting to null");
            return new FloorTile(); //TODO: Make an ErrorTile? (Probably)
        }
    }
}
