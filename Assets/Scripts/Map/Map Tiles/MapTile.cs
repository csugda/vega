using System;
using UnityEngine;

namespace Assets.Scripts.Map.Map_Tiles
{
    [Serializable]
    public class MapTile : ISharedSeed
    {
        public GameObject Tile;
        public int Weight;
        public TileType Type;
        public bool Rotate;

        public virtual void InstantiateTile(Vector3 position, Transform parentTransform)
        {
            UnityEngine.Debug.Log("Using Seed: " + MapRandom.GetCurrentSeed());
            var obj = GameObject.Instantiate(Tile, position, Quaternion.Euler(0,(MapRandom.NextInt(0,3)*90),0));
            obj.transform.parent = parentTransform;
        }

        void ISharedSeed.SetSharedSeed(bool willGenerateSeed)
        {
            throw new NotSupportedException();
        }
    }
}