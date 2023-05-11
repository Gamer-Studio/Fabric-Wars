using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace FabricWars.Worlds
{
    [Serializable]
    public class Chunk
    {
#if UNITY_EDITOR
        public int s_size;
#endif
        public readonly int size = 20;
        public Vector3Int position;
        private Tilemap _tilemap;
        private TileBase[] tiles;

        public Chunk(int size, Vector3Int position, Tilemap tilemap, TileBase[] tiles)
        {
#if UNITY_EDITOR
            s_size = size;
#endif
            
            this.size = size;
            this.position = position;
            this._tilemap = tilemap;
            this.tiles = tiles;
            Generate();
        }

        private void Generate()
        {
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    var tilePosition = new Vector3Int(position.x * size + x, position.y * size + y, 0);
                    _tilemap.SetTile(tilePosition, GetTileAt(tilePosition));
                }
            }
        }

        public void Clear()
        {
            var bounds = new BoundsInt(position * size, new Vector3Int(size, size, 1));
            _tilemap.SetTilesBlock(bounds, new TileBase[size * size]);
        }

        private TileBase GetTileAt(Vector3Int position)
        {
            // 여기서 원하는 타일맵 생성 알고리즘을 구현하세요.
            // 예를 들어, 무작위 타일을 선택할 수 있습니다.
            var randomIndex = Random.Range(0, tiles.Length);
            return tiles[randomIndex];
        }
    }
}