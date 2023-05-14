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
        public readonly int size;
        public Vector3Int position;
        protected Tilemap _layer1, _layer2, _layer3, _layer4;
        private TileBase[] _tiles;

        public Chunk(int size, Vector3Int position, TileBase[] tiles, Tilemap layer1, Tilemap layer2, Tilemap layer3, Tilemap layer4)
        {
#if UNITY_EDITOR
            s_size = size;
#endif
            
            this.size = size;
            this.position = position;
            this._layer1 = layer1;
            this._layer2 = layer2;
            this._layer3 = layer3;
            this._layer4 = layer4;
            this._tiles = tiles;
        }

        public virtual void Generate()
        {
            for (var x = 0; x < size; x++)
            {
                for (var y = 0; y < size; y++)
                {
                    var tilePosition = new Vector3Int(position.x * size + x, position.y * size + y, 0);
                    
                    _layer1.SetTile(tilePosition, GetTileAt(tilePosition));
                }
            }
        }

        public void Clear()
        {
            var bounds = new BoundsInt(position * size, new Vector3Int(size, size, 1));
            _layer1.SetTilesBlock(bounds, new TileBase[size * size]);
        }

        private TileBase GetTileAt(Vector3Int position)
        {
            // 여기서 원하는 타일맵 생성 알고리즘을 구현하세요.
            // 예를 들어, 무작위 타일을 선택할 수 있습니다.
            var randomIndex = Random.Range(0, _tiles.Length);
            return _tiles[randomIndex];
        }
    }
}