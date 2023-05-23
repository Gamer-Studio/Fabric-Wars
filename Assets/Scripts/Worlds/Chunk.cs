using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = UnityEngine.Random;

namespace FabricWars.Worlds
{
    [Serializable]
    public class Chunk
    {
        public Vector3Int position;
        public readonly World world;
        private int chunkSize => world.chunkSize;
        private List<Tilemap> layers => world.tilemapLayers;
        private TileBase[] _tiles;

        public Chunk(World world, Vector3Int position, TileBase[] tiles)
        {
            this.position = position;
            this._tiles = tiles;
            this.world = world;
        }

        public virtual void Generate()
        {
            for (var x = 0; x < chunkSize; x++)
            {
                for (var y = 0; y < chunkSize; y++)
                {
                    var tilePosition = new Vector3Int(position.x * chunkSize + x, position.y * chunkSize + y, 0);
                    
                    layers[1].SetTile(tilePosition, GetTileAt(tilePosition));
                }
            }
        }

        public void Clear()
        {
            var bounds = new BoundsInt(position * chunkSize, new Vector3Int(chunkSize, chunkSize, 1));
            layers[1].SetTilesBlock(bounds, new TileBase[chunkSize * chunkSize]);
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