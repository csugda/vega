using UnityEngine;
using System.Linq;
using System;

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

        public event EventHandler<MapEventArgs> OnInstantiationDone;

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
        /// </summary>
        public void InstantiateEntireMap()
        {
            foreach (TileType type in Enum.GetValues(typeof(TileType)))
            {
                DoActionForAllTilesOfType(InstantiateTile, type);
                RaiseOnInstantiationDone(type);
            }
        }

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
        }

        private void InstantiateTile(TileType tile, Vector3 pos)
        {
            // TODO: Add switch for different object placements such as walls... etc
            var obj = mapTileSet.GetTileOfType(TileTypeMap[(int)pos.x, (int)pos.z]);
            obj = Instantiate(obj, GetScaledPositionVector(obj, pos), Quaternion.identity);
            obj.transform.parent = transform;
        }

        private void RaiseOnInstantiationDone(TileType type)
        {
            var handler = OnInstantiationDone;
            if (handler == null) return;

            handler(this, new MapEventArgs(type));
        }

        private Vector3 GetScaledPositionVector(GameObject obj, Vector3 pos)
        {
            Bounds bounds = new Bounds();

            foreach(var rend in obj.GetComponents<Renderer>())
            {
                bounds.Encapsulate(rend.bounds);
            }
            return Vector3.Scale(bounds.size, pos);
        }

        public void OnDestroy()
        {
            foreach (Transform child in transform)
                Destroy(child.gameObject);
        }
    }
}

