using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Utils.Extensions
{
    public static class TilemapExtensions
    {
        public static void SetTiles(this Tilemap tilemap, Vector2Int start, Vector2Int end, TileBase tile)
        {
            for (var x = start.x; x <= end.x; x++)
            {
                for (var y = start.y; y <= end.y; y++)
                {
                    tilemap.SetTile(new Vector3Int(x, y), tile);
                }
            }
        }
    }
}