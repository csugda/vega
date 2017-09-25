using System;

namespace Assets.Scripts.Map
{
    
    /// <summary>
    /// Generates a randomized map given certain construction parameters.
    /// </summary>
    public class MapGenerator : ISharedSeed
    {
        protected TileType[,] GeneratorMap;
        protected MapParameters MapParams;

        public MapGenerator(MapParameters mapParams)
        {
            MapParams = mapParams;
            SetSharedSeed(MapParams.GenerateRandomMap);
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
        private void CreateMapSectors()
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

        public void SetSharedSeed(bool willGenerateSeed)
        {
            UnityEngine.Debug.Log("Seed is: " + MapParams.Seed);
            if (willGenerateSeed && !MapRandom.SeedIsGenerated)
            {
                MapParams.Seed = MapRandom.GenerateAndGetNewSeed();
            }
            else
            {
                
                MapRandom.SetManualSeed(MapParams.Seed);
            }
        }
    }
}

