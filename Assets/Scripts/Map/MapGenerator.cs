using System;

namespace Assets.Scripts.Map
{
    
    /// <summary>
    /// Generates a randomized map given certain construction parameters.
    /// </summary>
    public class MapGenerator
    {
        protected TileType[,] GeneratorMap;
        protected MapParameters MapParams;

        public MapGenerator(MapParameters mapParams)
        {
            MapParams = mapParams;
        }

        /// <summary>
        /// Generate a map randomly
        /// </summary>
        /// <param name="map">Map to Generate</param>
        public TileType[,] GetGeneratedMap()
        {
            GeneratorMap = new TileType[MapParams.Height, MapParams.Width];

            return GeneratorMap;
        }

        private void PlaceTiles(Func<TileType[,]> func)
        {
            for (int row = 0; row < GeneratorMap.GetLength(0); ++row)
            {
                for (int col = 0; col < GeneratorMap.GetLength(1); ++col)
                {
                    GeneratorMap[row, col] = TileType.Floor;
                }
            }
        }     
    }
}

