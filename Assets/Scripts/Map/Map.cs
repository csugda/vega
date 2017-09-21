using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Map
{
    /// <summary>
    /// The Map class holds all static information about the map.
    /// This includes tile types, tile sets, height and width.
    /// NOTE: The Map should not be responsible for handling any non-static data!
    /// </summary>
    public class Map : MonoBehaviour
    {
        private TileType[][] map;

        public Map(int width, int height)
        {
        }

        // Use this for initialization
        void Start()
        {
        }

        // Update is called once per frame
        void Update()
        {
        }

        /// <summary>
        /// Generates this map randomly based on the set parameters and the given MapGenerator
        /// </summary>
        /// <param name="mapGen"></param>
        public void Generate(MapGenerator mapGen)
        {
            mapGen.GenerateMap(this);
        }
    }
}

