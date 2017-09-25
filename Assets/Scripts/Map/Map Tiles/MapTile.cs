using System;
using UnityEngine;

namespace Assets.Scripts.Map.Map_Tiles
{
    [Serializable]
    public class MapTile
    {
        public GameObject Tile;
        public int Weight;
        public TileType Type;

        public virtual void InstantiateTile(Vector3 position, Transform parentTransform)
        {
            var obj = GameObject.Instantiate(Tile, position, Quaternion.identity);
            obj.transform.parent = parentTransform;
        }
    }
}
