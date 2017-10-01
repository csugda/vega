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

        public void GenerateMap()
        {
            GeneratorMap = new TileType[(int)MapParams.MapBounds.x, (int)MapParams.MapBounds.z];
            SectorMap = new int[(int)MapParams.MapBounds.x, (int)MapParams.MapBounds.z];

            CreateMapSectors();
            PlaceBorderTiles();
        }

        /// <summary>
        /// Generate a map randomly
        /// </summary>
        public TileType[,] GetGeneratedMap()
        {
            return GeneratorMap;
        }

        public int[,] GetSectorMap()
        {
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
                if (mapPlaceStart.x + MapParams.MinimumRoomSize.x > mapSize.x)
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

                if (mapPlaceStart.z + MapParams.MinimumRoomSize.z > mapSize.z)
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

        private void PlaceBorderTiles()
        {
            for (int i = 0; i < MapParams.MapBounds.x; ++i)
            {
                for (int j = 0; j < MapParams.MapBounds.z; ++j)
                {
                    int up = i - 1;
                    int down = i + 1;
                    int left = j - 1;
                    int right = j + 1;

                    if (up < 0 || down > MapParams.MapBounds.x-1 ||
                       left < 0 || right > MapParams.MapBounds.z-1)
                    {
                        GeneratorMap[i, j] = TileType.Border;
                        continue;
                    }

                    int currentSect = SectorMap[i, j];

                    if (currentSect != SectorMap[up,j]       ||
                        currentSect != SectorMap[i, left]    ||
                        currentSect != SectorMap[i, right]   ||
                        currentSect != SectorMap[down, j])
                    {
                        GeneratorMap[i, j] = TileType.Border;
                    }
                }
            }
        }
    }
}

