using UnityEngine;
using System.Linq;
using System;

namespace Assets.Scripts.Map.Map_Tiles
{
    /// <summary>
    /// The Map class holds all static information about the map.
    /// This includes height and width, tile type map, tile set(s)
    /// </summary>
    public class Map : MonoBehaviour, ISharedSeed
    {
        public TileType[,] TileTypeMap;
        public int[,] SectorMap;
        public MapTileSet[] SectorTileSets;

        public MapParameters MapParams;

        public MapGenerator MapGen;

        public event EventHandler<MapEventArgs> InstantiationHandler;
        public event EventHandler MapHandler;

        private void Start()
        {
            SectorMap = new int[MapParams.Width, MapParams.Height];
            for(int i = 0; i < MapParams.Width; ++i)
            {
                for(int j = 0; j < MapParams.Height; ++j)
                {
                    SectorMap[i, j] = 0;
                }
            }
            GenerateMap();
        }

        /// <summary>
        /// Regenerate a new map and destroy the current children of this map
        /// if it has any
        /// </summary>
        public void GenerateMap()
        {
            TileTypeMap = new TileType[MapParams.Width, MapParams.Height];

            SetSharedSeed(MapParams.GenerateRandomMap);

            MapGen = new MapGenerator(MapParams);
            TileTypeMap = MapGen.GetGeneratedMap();

            foreach(Transform child in transform)
                Destroy(child.gameObject);
            InstantiateMap();
        }

        /// <summary>
        /// Instantiate the entire map using every type specified in (enum)TileType
        /// Raises OnInstantiationDone event
        /// </summary>
        public void InstantiateMap()
        {
            foreach (TileType type in Enum.GetValues(typeof(TileType)))
            {
                DoActionForAllTilesOfType(InstantiateTile, type);
                OnInstantiationDone(type);
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
                from x in Enumerable.Range(0, MapParams.Height)
                from z in Enumerable.Range(0, MapParams.Width)
                where TileTypeMap[x, z] == type
                select new { X = x, Z = z };

            foreach(var tile in typeQuery)
            {
                var actOnTile = TileTypeMap[tile.X, tile.Z];
                action(actOnTile, new Vector3(tile.X, 0, tile.Z));
            }
            OnMapActionComplete();
        }

        private void InstantiateTile(TileType tile, Vector3 pos)
        {
            var sectorID = SectorMap[(int)pos.x, (int)pos.z];
            var obj = SectorTileSets[sectorID].GetTileOfType(tile);
            obj.InstantiateTile(GetScaledPositionVector(obj.Tile,pos), transform);
        }

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
            Bounds bounds = new Bounds();
            foreach(var rend in obj.GetComponentsInChildren<Renderer>())
            {
                bounds.Encapsulate(rend.bounds);
            }
            return Vector3.Scale(new Vector3(10,0,10), pos);
        }

        public void SetSharedSeed(bool willGenerateSeed)
        {
            if (willGenerateSeed && !MapRandom.SeedIsGenerated)
            {
                MapParams.Seed = MapRandom.GenerateAndGetNewSeed();
            }
            else
            {
                UnityEngine.Debug.Log("Setting seed to: " + MapParams.Seed);
                MapRandom.SetManualSeed(MapParams.Seed);
            }
        }

        public void OnDestroy()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}

