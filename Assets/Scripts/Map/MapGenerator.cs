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
            Vector3 roomSize = Vector3.zero;
            Vector3 mapPlaceStart = Vector3.zero;
            var mapSize = MapParams.MapBounds;
            int sectorID = 1;
            for(sectorID = 1; sectorID <= MapParams.MapSectors; ++sectorID)
            {
                bool xPlaced = false;
                bool zPlaced = false;

                roomSize = CreateSector();

                int row = 0;
                int col = 0;
                if (mapPlaceStart.x + roomSize.x >= mapSize.x)
                {
                    mapPlaceStart.x = 0;
                    for (row = 0; row < mapSize.z; ++row)
                    {
                        if (SectorMap[0, row] == 0 && (row + roomSize.z) < mapSize.z)
                        {
                            xPlaced = true;
                            mapPlaceStart.z = row;
                            break;
                        }
                    }
                }
                else xPlaced = true;

                if (mapPlaceStart.z + roomSize.z >= mapSize.z)
                {
                    mapPlaceStart.z = 0;
                    for (col = 0; col < mapSize.x; ++col)
                    {
                        if (SectorMap[col, 0] == 0 && (col + roomSize.x) < mapSize.x)
                        {
                            zPlaced = true;
                            mapPlaceStart.x = col;
                            break;
                        }
                    }
                }
                else zPlaced = true;

                if (xPlaced == false || zPlaced == false) continue;

                PlaceSector(mapPlaceStart,roomSize,sectorID);

                PlaceSectorBordersAndDoorways(mapPlaceStart, roomSize);
                mapPlaceStart.x += roomSize.x;
            }
        }

        private Vector3 CreateSector()
        {
            Vector3 roomBounds = Vector3.zero;
            Vector3 minSize = MapParams.MinimumRoomSize;
            Vector3 maxSize = MapParams.MaximumRoomSize;

            roomBounds.x = minSize.x + rand.GetInt((int)(maxSize.x - minSize.x));
            roomBounds.z = minSize.z + rand.GetInt((int)(maxSize.z - minSize.z));

            return roomBounds;
        }

        /// <summary>
        /// Place the actual sector that was randomly bounded previously.
        /// </summary>
        /// <param name="startTile"></param>
        /// <param name="roomSize"></param>
        /// <param name="sectorID"></param>
        /// <returns></returns>
        private void PlaceSector(Vector3 startTile, Vector3 roomSize, int sectorID)
        {
            Vector3 endTile = startTile + roomSize;
             
            for (int i = (int)startTile.x; i < endTile.x; ++i)
            {
                for (int j = (int)startTile.z; j < endTile.z; ++j)
                {
                    if(i >= MapParams.MapBounds.x || j >= MapParams.MapBounds.z)
                    {
                        Debug.LogError(startTile + " end: " + endTile);
                    }
                    SectorMap[i, j] = sectorID;
                }
            }
        }

        private void PlaceSectorBordersAndDoorways(Vector3 startTile, Vector3 sectorBounds)
        {
            Vector3 endTile = startTile + sectorBounds;
            var TopBotStep = Vector3.right;
            var LeftRightStep = Vector3.forward;

            PlaceAllBorders(startTile, endTile, TopBotStep);
            //PlaceAllBorders(startTile, endTile, LeftRightStep);
        }

        private void PlaceAllBorders(Vector3 lowBound, Vector3 highBound, Vector3 step)
        {
            Vector3 totalStep = Vector3.zero;
            while (lowBound.x + totalStep.x <= highBound.x || lowBound.z + totalStep.z <= highBound.z)
            {
                var lowStep = lowBound + totalStep;
                if (lowStep.x < 0 || lowStep.z < 0 || lowStep.x >= MapParams.MapBounds.x || lowStep.z >= MapParams.MapBounds.z)
                {
                    break;
                }
                var currLowTile = new Vector3(lowStep.x, 0, lowBound.z);
                var currHighTile = new Vector3(lowStep.x, 0, highBound.z);

                totalStep += step;

                SetMapTileToType(currLowTile, TileType.Border);
                SetMapTileToType(currHighTile, TileType.Border);
            }
        }

        private void SetMapTileToType(Vector3 tile, TileType type)
        {
            Debug.Log(tile);
            GeneratorMap[(int)tile.x, (int)tile.z] = type;
        }

        private TileType GetMapTileType(Vector3 tile)
        {
            return GeneratorMap[(int)tile.x, (int)tile.z];
        }
    }
}

