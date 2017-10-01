using System;
using System.Linq;
using UnityEngine;

namespace Assets.Scripts.Map
{
    /// <summary>
    /// Generates a randomized map given certain construction parameters.
    /// </summary>
    public class MapGenerator
    {
        protected TileType[,] GeneratorMap;
        protected int[,] SectorMap;
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
        public TileType[,] GetGeneratedMap()
        {
            GeneratorMap = new TileType[(int)MapParams.MapBounds.x, (int)MapParams.MapBounds.z];
            //TODO: WALL THINGIES
            return GeneratorMap;
        }

        public int[,] GetSectorMap()
        {
            SectorMap = new int[(int)MapParams.MapBounds.x, (int)MapParams.MapBounds.z];
            CreateMapSectors();
            return SectorMap;
        }

        /// <summary>
        /// Split the map into MapParams.MapSectors sub-sectors.
        /// Will only split if all new sub-sectors would remain 
        /// at least a size of MapParams.MinimumRoomSize 
        /// </summary>
        private void CreateMapSectors()
        {
            Vector3 mapBoundsUsed = Vector3.zero;
            Vector3 mapPlaceStart = Vector3.zero;
            var mapSize = MapParams.MapBounds;
            int sectorID = 1;
            for(sectorID = 1; sectorID <= MapParams.MapSectors; ++sectorID)
            {
                mapPlaceStart.x += mapBoundsUsed.x;

                int row = 0;
                int col = 0;
                if(mapPlaceStart.x >= mapSize.x)
                {
                    mapPlaceStart.x = 0;
                    for(row = 0; row < mapSize.z; ++row)
                    {
                        if(SectorMap[0, row] == 0)
                        {
                            mapPlaceStart.z = row;
                            break;
                        }
                    }
                }

                if (mapPlaceStart.z >= mapSize.z)
                {
                    mapPlaceStart.z = 0;
                    for (col = 0; col < mapSize.x; ++col)
                    {
                        if (SectorMap[col, 0] == 0)
                        {
                            mapPlaceStart.x = col;
                            break;
                        }
                    }
                }

                mapBoundsUsed = CreateSector(mapSize - mapBoundsUsed);
                PlaceSector(mapPlaceStart,mapBoundsUsed,sectorID);
            }
        }

        private Vector3 CreateSector(Vector3 boundsAvailable)
        {
            Vector3 roomBounds = Vector3.zero;
            Vector3 minSize = MapParams.MinimumRoomSize;

            roomBounds.x = minSize.x + rand.GetInt((int)(boundsAvailable.x - minSize.x));
            roomBounds.z = minSize.z + rand.GetInt((int)(boundsAvailable.z - minSize.z));

            if(boundsAvailable.x - roomBounds.x <= minSize.x)
            {
                roomBounds.x = boundsAvailable.x;
            }
            if(boundsAvailable.z - roomBounds.z <= minSize.z)
            {
                roomBounds.z = boundsAvailable.z;
            }

            return roomBounds;
        }

        private void PlaceSector(Vector3 startTile, Vector3 roomSize, int sectorID)
        {
            Vector3 endTile = startTile + roomSize;
            for (int i = (int)startTile.x; i < endTile.x; ++i)
            {
                if (i >= MapParams.MapBounds.x) break;
                for(int j = (int)startTile.z; j < endTile.z; ++j)
                {
                    if (j >= MapParams.MapBounds.z) break;
                    SectorMap[i, j] = sectorID;
                }
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

