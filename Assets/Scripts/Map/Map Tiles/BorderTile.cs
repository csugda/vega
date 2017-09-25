using UnityEngine;

namespace Assets.Scripts.Map.Map_Tiles
{
    class BorderTile : MapTile
    {
        public BorderTile()
        {
            Type = TileType.Border;
        }

        public override void InstantiateTile(Vector3 position, Transform parentTransform, long seed)
        {
            base.InstantiateTile(position, parentTransform, seed);
        }
    }
}
