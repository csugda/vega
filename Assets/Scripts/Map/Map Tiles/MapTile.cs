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
        public bool Rotate;

        public virtual void InstantiateTile(Vector3 position, Transform parentTransform, long seed)
        {
            var obj = GameObject.Instantiate(Tile, position, Quaternion.Euler(0,(UnityEngine.Random.Range(0,3)*90),0));
            obj.transform.parent = parentTransform;
        }
    }
}