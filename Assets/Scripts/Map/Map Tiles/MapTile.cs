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

        public virtual void InstantiateTile(Vector3 position, Transform parentTransform, MapRandom gen)
        {
            var obj = GameObject.Instantiate(Tile, position, Rotate ? Quaternion.Euler(0, (gen.GetInt(0, 3) * 90), 0) : Quaternion.identity, parentTransform);
            obj.transform.parent = parentTransform;
        }
    }
}