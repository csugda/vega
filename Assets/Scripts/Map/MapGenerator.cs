using System;
using System.Collections.Generic;
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
            Vector3 prevRoomStart = Vector3.zero;
            Vector3 prevRoomSize = Vector3.zero;
            Vector3 currRoomStart = Vector3.zero;
            var mapSize = MapParams.MapBounds;
            int sectorID = 1;
            for(sectorID = 1; sectorID <= MapParams.MapSectors; ++sectorID)
            {
                Vector3 currRoomSize = Vector3.zero;

                var randZ = Math.Max(0, prevRoomStart.z + (1-rand.GetInt(4)));

                currRoomStart = new Vector3(prevRoomStart.x + prevRoomSize.x, 0, randZ);
                Vector3 currBoundsLeft = mapSize - currRoomStart;

                if(!MinimumSectorCanFit(currBoundsLeft))
                {
                    int z = (int)MapParams.MinimumRoomSize.z;
                    var startX = rand.GetInt((int)MapParams.MinimumRoomSize.x);
                    int x = startX;
                    bool placed = false;
                    while(placed == false)
                    {
                        while (SectorMap[x, z] != 0)
                        {
                            ++z;
                            if (z >= mapSize.z) return;
                        }

                        x = startX;
                        while (x <= MapParams.MinimumRoomSize.x)
                        {
                            ++x;
                            if (SectorMap[x, z] != 0) break;
                        }

                        if (SectorMap[x, z] == 0)
                        {
                            currRoomStart = new Vector3(startX, 0, z);
                            placed = true;
                        }
                    }
                    currBoundsLeft = mapSize - currRoomStart;
                }
                if (!MinimumSectorCanFit(currBoundsLeft)) break;

                currRoomSize = CreateSector(currBoundsLeft);

                PlaceSector(currRoomStart, currRoomSize, sectorID);
                PlaceSectorBordersAndDoorways(currRoomStart, prevRoomSize);
                prevRoomStart = currRoomStart;
                prevRoomSize = currRoomSize;
            }
        }

        private void PlaceSectorBordersAndDoorways(Vector3 currRoomStart, Vector3 prevRoomSize)
        {
        }

        private bool MinimumSectorCanFit(Vector3 boundsLeft)
        {
            return (boundsLeft.x >= MapParams.MinimumRoomSize.x &&
                    boundsLeft.z >= MapParams.MinimumRoomSize.z);
        }

        private Vector3 CreateSector(Vector3 boundsLeft)
        {
            Vector3 roomBounds = Vector3.zero;
            Vector3 minSize = MapParams.MinimumRoomSize;
            Vector3 maxSize = MapParams.MaximumRoomSize;

            var randX = Math.Min(rand.GetInt((int)(maxSize.x - minSize.x)), boundsLeft.x - minSize.x);
            var randZ = Math.Min(rand.GetInt((int)(maxSize.z - minSize.z)), boundsLeft.z - minSize.z);

            roomBounds.x = minSize.x + randX;
            roomBounds.z = minSize.z + randZ;

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
            Vector3 mapSize = MapParams.MapBounds;
            Dictionary<int, List<Vector3>> wallDoorwayDict = new Dictionary<int, List<Vector3>>();

            for (int i = (int)startTile.x; i < endTile.x; ++i)
            {
                for (int j = (int)startTile.z; j < endTile.z; ++j)
                {
                    SectorMap[i, j] = sectorID;
                    var tile = new Vector3(i, 0, j);
                    if (i == startTile.x || i == endTile.x || j == startTile.z || j == endTile.z)
                    {
                        SetMapTileToType(tile, TileType.Border);
                        if (!((i == startTile.x || i == endTile.x) && (j == startTile.z || j == endTile.z)))
                        {
                            int otherID = 0;
                            int leftID= (i == 0 ? 0 : SectorMap[i - 1, j] == sectorID ? 0 : SectorMap[i - 1, j]);
                            int topID = (j == 0 ? 0 : SectorMap[i, j - 1] == sectorID ? 0 : SectorMap[i, j - 1]);
                            int rightID = (i == mapSize.x ? 0 : SectorMap[i + 1, j] == sectorID ? 0 : SectorMap[i + 1, j]);
                            int botID = (j == mapSize.z ? 0 : SectorMap[i, j + 1] == sectorID ? 0 : SectorMap[i, j + 1]);

                            int sector =Math.Max(leftID, Math.Max(topID, Math.Max(rightID, botID)));

                            if (wallDoorwayDict[sector] == null)
                            {
                                wallDoorwayDict[sector] = new List<Vector3>();
                            }

                            wallDoorwayDict[sector].Add(tile);   
                        }
                    }
                    else SetMapTileToType(tile, TileType.Floor);



                    //TODO: for each sector, make random doorways.
                }
            }

            foreach (var tileEntry in wallDoorwayDict)
            {
                int maxRange = tileEntry.Value.Count();
                Vector3 doorwayTile = tileEntry.Value.ElementAt(rand.GetInt(maxRange));

            }
        }

        private bool IsTileOutOfMapBounds(Vector3 tile)
        {
            if (tile.x < 0 || tile.z < 0 || tile.x >= MapParams.MapBounds.x || tile.z >= MapParams.MapBounds.z)
            {
                return true;
            }
            return false;
        }

        private Vector3 GetDiffSectorDirection(Vector3 tile)
        {
            var query = 
                from vector in Vector3.
            return Vector3.zero;
        }


        private void SetMapTileToType(Vector3 tile, TileType type)
        {
            if (IsTileOutOfMapBounds(tile)) return;

            GeneratorMap[(int)tile.x, (int)tile.z] = type;
        }

        private int GetSectorIDfromTile(Vector3 tile)
        {
            if (IsTileOutOfMapBounds(tile)) return 0;

            return SectorMap[(int)tile.x, (int)tile.z];
        }

        private TileType GetMapTileType(Vector3 tile)
        {
            if (IsTileOutOfMapBounds(tile)) return TileType.Floor;
            return GeneratorMap[(int)tile.x, (int)tile.z];
        }
    }
}

