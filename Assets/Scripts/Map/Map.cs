using UnityEngine;
using System.Linq;
using System;
using UnityEngine.Events;

namespace Assets.Scripts.Map
{
    public class MapEvent : UnityEvent { }

    /// <summary>
    /// The Map class holds all static information about the map.
    /// This includes height and width, tile type map, tile set(s)
    /// </summary>
    public class Map : MonoBehaviour
    {
        public TileType[,] TileTypeMap;
        public int[,] SectorMap;
        public Map_Tiles.MapTileSet[] SectorTileSets;
        public MapEvent onMapChanged = new MapEvent();
        public MapParameters MapParams;

        MapRandom rand;

        public MapGenerator MapGen;

        public event EventHandler<MapEventArgs> InstantiationHandler;
        public event EventHandler MapHandler;

        private void Start()
        {
            GenerateMap();
        }

        /// <summary>
        /// Regenerate a new map and destroy the current children of this map
        /// if it has any
        /// </summary>
        public void GenerateMap()
        {
            
            if (MapParams.MaximumRoomSize.x < MapParams.MinimumRoomSize.x)
            {
                MapParams.MaximumRoomSize.x = MapParams.MinimumRoomSize.x;
            }
            if (MapParams.MaximumRoomSize.z < MapParams.MinimumRoomSize.z)
            {
                MapParams.MaximumRoomSize.z = MapParams.MinimumRoomSize.z;
            }
            if (MapParams.GenerateRandomMap)
            {
                MapParams.Seed = UnityEngine.Random.Range(0, Int32.MaxValue);
            }
            rand = new MapRandom(MapParams.Seed);

            MapGen = new MapGenerator(MapParams, rand.GetInt(Int32.MaxValue));
            MapGen.GenerateMap();

            SectorMap = MapGen.GetSectorMap();            
            TileTypeMap = MapGen.GetGeneratedMap();
            
            foreach(Transform child in transform)
                Destroy(child.gameObject);

            InstantiateMap();
            onMapChanged.Invoke();
        }

        /// <summary>
        /// Instantiate the entire map using every type specified in (enum)TileType
        /// Raises OnInstantiationDone event
        /// </summary>
        public void InstantiateMap()
        {
            MapRandom TileRotationRand = new MapRandom(rand.GetInt(Int32.MaxValue));
            MapRandom TileTypeRand = new MapRandom(rand.GetInt(Int32.MaxValue));
            for (int x = 0; x < MapParams.MapBounds.x; ++x)
                for (int z = 0; z < MapParams.MapBounds.z; ++z)
                {
                    if (SectorMap[x, z] == 0) continue;
                    SectorTileSets[SectorMap[x, z]-1].
                        GetTileOfType(TileTypeMap[x, z], TileTypeRand).
                        InstantiateTile(new Vector3(10 * x, 0, 10 * z), this.transform, TileRotationRand);
                }
        }

        /// <summary>
        /// For all tiles of a given type in the map, do the passed action.
        /// Raises the OnMapActionComplete event.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="type"></param>
        public void DoActionForAllTilesOfType(Action<TileType, Vector3> action, TileType type)
        {
            var typeQuery =
                from x in Enumerable.Range(0, (int)MapParams.MapBounds.x)
                from z in Enumerable.Range(0, (int)MapParams.MapBounds.z)
                where TileTypeMap[x, z] == type
                select new { X = x, Z = z };

            foreach(var tile in typeQuery)
            {
                var actOnTile = TileTypeMap[tile.X, tile.Z];
                action(actOnTile, new Vector3(tile.X, 0, tile.Z));
            }
            OnMapActionComplete();
        }

        //private void InstantiateTile(TileType tile, Vector3 pos)
        //{
        //    var sectorID = SectorMap[(int)pos.x, (int)pos.z];
        //    var obj = SectorTileSets[sectorID].GetTileOfType(tile);
        //    obj.InstantiateTile(GetScaledPositionVector(obj.Tile,pos), transform);
        //}

        private void OnInstantiationDone(TileType type)
        {
            var handler = InstantiationHandler;
            if (handler == null) return;

            handler(this, new MapEventArgs(type));
        }

        private void OnMapActionComplete()
        {
            var handler = MapHandler;
            if (handler == null) return;

            handler(this, new EventArgs());
        }

        private Vector3 GetScaledPositionVector(GameObject obj, Vector3 pos)
        {
            return Vector3.Scale(new Vector3(10,0,10), pos);
        }

        public Vector3 MapPositionFromWorld (Vector3 worldPos)
        {
            return new Vector3((worldPos.x / 10), 0, (worldPos.z / 10));
        }

        public void OnDestroy()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}

