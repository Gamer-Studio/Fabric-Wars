using System;
using System.Collections.Generic;
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
    }
}