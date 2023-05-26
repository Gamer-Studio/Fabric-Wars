using System.Linq;
using FabricWars.Jobs;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Utils.Extensions
{
    public static class TilemapExtensions
    {
        public static void SetTiles(this Tilemap tilemap, Vector3Int start, Vector3Int end, TileBase tile)
        {
            var range = Vector3IntRange.Create(start, end).ToArray();
            var tiles = Enumerable.Repeat(tile, range.Length).ToArray();
            tilemap.SetTiles(range, tiles);
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