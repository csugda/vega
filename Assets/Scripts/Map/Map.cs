using UnityEngine;

namespace Assets.Scripts.Map
{
    /// <summary>
    /// The Map class holds all static information about the map.
    /// This includes height and width, tile type map, tile set(s)
    /// </summary>
    public class Map : MonoBehaviour
    {
        public TileType[,] TileTypeMap;

        private Bounds TotalFloorBounds;

        public bool GenerateMap;

        public MapParameters MapParams;

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
                //DO OTHER THINGS
            }
            InstantiateMap();
        }

        public void NewMap()
        {
            TileTypeMap = MapGenerator.GenerateMap(MapParams);
            foreach (Transform child in transform)
                Destroy(child.gameObject);
            InstantiateMap();
        }
        /// <summary>
        /// Instantiate map objects based on TileTypeMap.
        /// </summary>
        public void InstantiateMap()
        {
            for (int row = 0; row < TileTypeMap.GetLength(0); ++row)
            {
                for (int col = 0; col < TileTypeMap.GetLength(1); ++col)
                {
                    Vector3 pos = new Vector3((float)row, 0, (float)col);

                    var obj = mapTileSet.GetTileOfType(TileTypeMap[row, col]);
                    obj = Instantiate(obj, GetScaledPositionVector(obj, pos), Quaternion.identity);
                    obj.transform.parent = transform;
                }
            }
        }

        private Vector3 GetScaledPositionVector(GameObject obj, Vector3 pos)
        {
            var rend = obj.GetComponent<Renderer>();
            return Vector3.Scale(rend.bounds.size, pos);
        }
    }
}

