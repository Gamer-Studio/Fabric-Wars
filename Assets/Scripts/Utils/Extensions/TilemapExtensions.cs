using System;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Utils.Extensions
{
    public static class TilemapExtensions
    {
        public static void SetTiles(this Tilemap tilemap, Vector3Int startPos, Vector3Int size, Func<Vector3Int, TileBase> generator)
        {
            var bound = new BoundsInt(startPos, size);
            var tiles = new TileBase[size.x * size.y];
            for (var x = startPos.x; x < startPos.x + size.x; x++)
            {
                for (var y = startPos.y; y < startPos.y + size.y; y++)
                {
                    tiles[x * size.y + y] = generator(new Vector3Int(x, y));
                }
            }
            tilemap.SetTilesBlock(bound, tiles.ToArray());
        }

        public static Vector3Int ToTilemapPosition(this Tilemap tilemap, Vector3 worldPosition)
        {
            var calibrated = worldPosition - tilemap.transform.position;
            
            return new Vector3Int(
                (int)calibrated.x - (calibrated.x < 0 ? 1 : 0),
                (int)calibrated.y - (calibrated.y < 0 ? 1 : 0)
            );
        }
        
        public static bool HasTile(this Tilemap tilemap, Vector3 worldPosition) => tilemap.GetTile(tilemap.ToTilemapPosition(worldPosition)) != null;
    }
}