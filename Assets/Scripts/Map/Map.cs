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

        public Bounds TotalFloorBounds;

        //The prefabs to choose from when creating the map
        public MapTileSet mapTileSet;

        public int Width;
        public int Height;

        private UnityEditor.AI.NavMeshBuilder navBuilder;

        private void Start()
        {
            TileTypeMap = new TileType[Width, Height];
        }

        public void InstantiateMap()
        {
            for (int row = 0; row < Height; ++row)
            {
                for (int col = 0; col < Width; ++col)
                {
                    float xPos = row;
                    float zPos = col;
                    Vector3 pos = new Vector3(xPos, 0, zPos);

                    var obj = mapTileSet.GetTileOfType(TileTypeMap[col, row]);
                    obj = Instantiate(obj, getScaledPositionVector(obj, pos), Quaternion.identity);
                    obj.transform.parent = transform;
                }
            }
        }

        private Vector3 getScaledPositionVector(GameObject obj, Vector3 pos)
        {
            var rend = obj.GetComponent<Renderer>();
            return Vector3.Scale(rend.bounds.size, pos);
        }
    }
}

