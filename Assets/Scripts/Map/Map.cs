using UnityEngine;
using System.Linq;
using System;
using Assets.Scripts.Map.Map_Tiles;

namespace Assets.Scripts.Map
{

    /// <summary>
    /// The Map class holds all static information about the map.
    /// This includes height and width, tile type map, tile set(s)
    /// </summary>
    public class Map : MonoBehaviour
    {
        public TileType[,] TileTypeMap;
        public bool GenerateMap;
        public MapParameters MapParams;

        public event EventHandler<MapEventArgs> InstantiationHandler;
        public event EventHandler MapHandler;

        //The prefabs to choose from when creating the map
        public MapTileSet mapTileSet;

        private void Start()
        {
            TileTypeMap = new TileType[MapParams.Width, MapParams.Height];
            if(GenerateMap)
            {
                TileTypeMap = MapGenerator.GenerateMap(MapParams);
            }
            else
            {
                TileTypeMap = null;
            }
            InstantiateEntireMap();
        }

        /// <summary>
        /// Regenerate a new map and destroy the current children of this map
        /// Generally called at runtime using the menu context button
        /// </summary>
        public void RegenerateMap()
        {
            TileTypeMap = MapGenerator.GenerateMap(MapParams);
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            InstantiateEntireMap();
        }
        /// <summary>
        /// Instantiate the entire map using every type in the TileType enum
        /// Raises OnInstantiationDone event
        /// </summary>
        public void InstantiateEntireMap()
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
            var obj = mapTileSet.GetTileOfType(tile);
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

        public void OnDestroy()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}

