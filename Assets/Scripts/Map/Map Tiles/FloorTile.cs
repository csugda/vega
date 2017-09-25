using UnityEngine;

namespace Assets.Scripts.Map.Map_Tiles
{
    public class FloorTile : MapTile
    {
        public FloorTile()
        {
            Type = TileType.Floor;
        }

        public override void InstantiateTile(Vector3 position, Transform parentTransform, long seed)
        {
            base.InstantiateTile(position, parentTransform, seed);
        }
    }
}
