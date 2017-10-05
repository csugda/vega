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
                PlaceSectorBordersAndDoorways(mapPlaceStart, mapBoundsUsed);
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

            if(roomBounds.x > MapParams.MaximumRoomSize.x)
            {
                roomBounds.x = MapParams.MaximumRoomSize.x;
            }

            if (roomBounds.z > MapParams.MaximumRoomSize.z)
            {
                roomBounds.z = MapParams.MaximumRoomSize.z;
            }

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
                if (i >= MapParams.MapBounds.x) break;
                for (int j = (int)startTile.z; j < endTile.z; ++j)
                {
                    if (j >= MapParams.MapBounds.z) break;
                    SectorMap[i, j] = sectorID;
                }
            }
        }

        private void PlaceSectorBordersAndDoorways(Vector3 startTile, Vector3 sectorBounds)
        {
            Vector3 endTile = startTile + sectorBounds;

            var TopBotStep = Vector3.right;
            var LeftRightStep = Vector3.forward;
            var TopBotEnd = new Vector3(endTile.x, 0, startTile.z);
            var LeftRightEnd = new Vector3(startTile.x, 0, endTile.z);

            ConnectPrevDoorwaysAndPlaceBorders(startTile, TopBotEnd, TopBotStep);
            ConnectPrevDoorwaysAndPlaceBorders(startTile, LeftRightEnd, LeftRightStep);
        }

        private void ConnectPrevDoorwaysAndPlaceBorders(Vector3 lowBound, Vector3 highBound, Vector3 step)
        {
            while (lowBound.magnitude < highBound.magnitude)
            {
                var currLowTile = new Vector3(lowBound.x, 0, lowBound.z);
                var currHighTile = new Vector3(highBound.x, 0, highBound.z);

                lowBound += step;

                var prevLowTile = currLowTile +   (step.x == 0 ? Vector3.left : Vector3.back);
                var prevHighTile = currHighTile + (step.x == 0 ? Vector3.right : Vector3.forward);

                if (prevLowTile.x < 0 || prevLowTile.z < 0) continue;
                if (prevHighTile.x > MapParams.MapBounds.x || prevHighTile.z > MapParams.MapBounds.z) continue;
                //left or up
                if (GetMapTileType(prevLowTile) == TileType.Doorway)
                {
                    SetMapTileToType(currLowTile, TileType.Doorway);
                }
                else SetMapTileToType(currLowTile, TileType.Border);

                //down or right
                if (GetMapTileType(prevHighTile) == TileType.Doorway)
                {
                    SetMapTileToType(currHighTile, TileType.Doorway);
                }
                else SetMapTileToType(currHighTile, TileType.Border);
            }
        }

        private void SetMapTileToType(Vector3 tile, TileType type)
        {
            GeneratorMap[(int)tile.x, (int)tile.z] = type;
        }

        private TileType GetMapTileType(Vector3 tile)
        {
            return GeneratorMap[(int)tile.x, (int)tile.z];
        }
    }
}

