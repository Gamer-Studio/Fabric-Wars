using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds.Chunks
{
    [Serializable]
    public class Chunk
    {
        public Vector3Int position;
        public readonly World world;
        private int chunkSize => world.chunkSize;
        private List<Tilemap> layers => world.tilemapLayers;

        public Chunk(World world, Vector3Int position)
        {
            this.position = position;
            this.world = world;
        }

        public void Clear()
        {
            var bounds = new BoundsInt(position * chunkSize, new Vector3Int(chunkSize, chunkSize, 1));
            layers[1].SetTilesBlock(bounds, new TileBase[chunkSize * chunkSize]);
        }
    }
}