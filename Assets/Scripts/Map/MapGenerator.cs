using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Map
{
    /// <summary>
    /// Generates a randomized map given certain construction parameters.
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        public MapParameters MapGeneratorParameters;

        private GameObject mapTileHolder;

        private Map map;

        private void Start()
        {
            map = GetComponent<Map>();

            mapTileHolder = new GameObject("MapTileHolder");
            GenerateMap(map, MapGeneratorParameters);
        }

        /// <summary>
        /// Generate a map randomly
        /// </summary>
        /// <param name="map">Map to Generate</param>
        // TODO: Add parameters for map generation such as map type, etc
        public void GenerateMap(Map map, MapParameters mapParams = null)
        {
            PlaceWalls();
            //TODO: GENERATE THE MAP HERE
        }

        private void PlaceFloor()
        {

        }

        private void PlaceWalls()
        {

            for(int i = 0; i < 4; ++i)
            {
                GameObject newWall = MapGeneratorParameters.WallTiles[i % 2];
                float xPos = i * newWall.transform.lossyScale.x;
                float yPos = 1;
                float zPos = i * newWall.transform.lossyScale.z;
                Vector3 position = new Vector3(xPos,yPos,zPos);
                newWall = Instantiate(newWall, position, Quaternion.identity) as GameObject;
                newWall.transform.parent = mapTileHolder.transform;
            }
        }
    }
}

