using System;
using System.Collections.Generic;
using FabricWars.Game.Entities;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds.Chunks
{
    [Serializable]
    public class Chunk
    {
        public readonly World world;
        private int chunkSize => world.chunkSize;
        private List<Tilemap> layers => world.tilemapLayers;
        public UnityEvent OnClear;
        public Vector3Int position;
        private readonly List<Entity> _entities = new();
        public Entity[] entities => _entities.ToArray();

        public Chunk(World world, Vector3Int position)
        {
            this.position = position;
            this.world = world;
        }

        public void Dispose()
        {
            layers[1].SetTilesBlock(new BoundsInt(position * chunkSize, new Vector3Int(chunkSize, chunkSize, 1)),
                new TileBase[chunkSize * chunkSize]);
            OnClear.Invoke();
        }
        
        public void Deconstruct(out Chunk chunk, out List<Tilemap> worldLayers, out int size)
        {
            chunk = this;
            worldLayers = layers;
            size = chunkSize;
        }

        /// <summary>
        /// 엔티티의 위치를 청크 내부 좌표로 이동하고 청크 오브젝트 목록에 포함함.
        /// </summary>
        /// <param name="chunkPosition">청크 내부 상대 좌표</param>
        /// <param name="entity">청크에 종속시킬 오브젝트</param>
        /// <returns></returns>
        public Entity SpawnEntity(Vector2 chunkPosition, Entity entity)
        {
            
            return entity;
        }
    }
}