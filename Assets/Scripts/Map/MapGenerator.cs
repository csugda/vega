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

        MapRandom rand;

        public MapGenerator(MapParameters mapParams, Int32 seed)
        {
            rand = new MapRandom(seed);
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

        /// <summary>
        /// Split the map into MapParams.MapSectors sub-sectors.
        /// Will only split if all new sub-sectors would remain 
        /// at least a size of MapParams.MinimumRoomSize 
        /// </summary>
        private void CreateMapSectors()//TODO
        {
            for(int sectorID = 0; sectorID < MapParams.MapSectors; ++sectorID)
            {

            }
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

