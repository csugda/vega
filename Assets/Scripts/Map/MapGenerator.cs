using UnityEngine;

namespace Assets.Scripts.Map
{
    /// <summary>
    /// Generates a randomized map given certain construction parameters.
    /// </summary>
    public static class MapGenerator
    {
        /// <summary>
        /// Generate a map randomly
        /// </summary>
        /// <param name="map">Map to Generate</param>
        public static TileType[,] GenerateMap(MapParameters mapParams)
        {
            TileType[,] TileTypeMap = new TileType[mapParams.Height, mapParams.Width];
            PlaceFloor(TileTypeMap);
            //PlaceOuterWalls();
            //PlaceInnerWalls();  
            return TileTypeMap;
        }

        private static void PlaceFloor(TileType[,] map)
        {
            for (int row = 0; row < map.GetLength(0); ++row)
            {
                for (int col = 0; col < map.GetLength(1); ++col)
                {
                    map[row,col] = TileType.Floor;
                }
            }
        }

        private static void PlaceOuterWalls(TileType[,] map)
        {
            for (int row = 0; row < map.GetLength(0); ++row)
            {
                for (int col = 0; col < map.GetLength(1); ++col)
                {
                    //map[col, row] = TileType.Floor;
                }
            }
        }

        private static void PlaceInnerWalls(TileType[,] map)
        {
            //TODO: Write inner walls loops
        }
    }
}

