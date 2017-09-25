using UnityEngine;

namespace Assets.Scripts.Map.Map_Tiles
{
    class OuterWallTile : MapTile
    {
        public OuterWallTile()
        {
            Type = TileType.OuterWall;
        }

        public override void InstantiateTile(Vector3 position, Transform parentTransform)
        {
            base.InstantiateTile(position, parentTransform);
        }
    }
}
