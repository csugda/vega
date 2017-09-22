using UnityEngine;

namespace Assets.Scripts.Map
{
    /// <summary>
    /// Generates a randomized map given certain construction parameters.
    /// </summary>
    public class MapGenerator : MonoBehaviour
    {
        public Map map;
        public MapParameters MapGeneratorParameters;
        
        private void Start()
        {
            GenerateMap(MapGeneratorParameters);
            map.InstantiateMap();
        }

        private void OnEnable()
        {
            map.Width = MapGeneratorParameters.Width;
            map.Height = MapGeneratorParameters.Height;
        }

        /// <summary>
        /// Generate a map randomly
        /// </summary>
        /// <param name="map">Map to Generate</param>
        public void GenerateMap(MapParameters mapParams)
        {
            PlaceFloor();
            //PlaceOuterWalls();
            //PlaceInnerWalls();    
        }

        private void PlaceFloor()
        {
            for (int row = 0; row < MapGeneratorParameters.Height; ++row)
            {
                for (int col = 0; col < MapGeneratorParameters.Width; ++col)
                {
                    map.TileTypeMap[col,row] = TileType.Floor;
                }
            }
        }

        private void PlaceOuterWalls()
        {
            for (int row = 0; row < MapGeneratorParameters.Height; ++row)
            {
                for (int col = 0; col < MapGeneratorParameters.Width; ++col)
                {
                    if (row == 0 || row == MapGeneratorParameters.Height ||
                        col == 0 || col == MapGeneratorParameters.Width)
                    {
                        map.TileTypeMap[col, row] = TileType.OuterWall;
                    }
                }
            }
        }

        private void PlaceInnerWalls()
        {
        }
    }
}

