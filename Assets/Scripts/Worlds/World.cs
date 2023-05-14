using System.Collections.Generic;
using FabricWars.Utils.Extensions;
using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds
{
    public class World : MonoBehaviour
    {
        public const int ChunkSize = 20;
        public List<Tilemap> tilemapLayers;
        public int loadDistance;
        public int unloadDistance;
        public TileBase[] tiles;

        [SerializeField] private Vector3Int previousPlayerChunk;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private SerializableDictionary<Vector3Int, Chunk> loadedChunks = new ();

        private void Start()
        {
            playerTransform = Camera.main.transform;
            previousPlayerChunk = WorldToChunkPosition(playerTransform.position);
            LoadChunks();
        }

        private void Update()
        {
            var currentChunk = WorldToChunkPosition(playerTransform.position);
            if (currentChunk != previousPlayerChunk)
            {
                previousPlayerChunk = currentChunk;
                LoadChunks();
            }
        }

        private static Vector3Int WorldToChunkPosition(Vector3 worldPos)
        {
            return new Vector3Int(Mathf.FloorToInt(worldPos.x / ChunkSize), Mathf.FloorToInt(worldPos.y / ChunkSize),
                0);
        }

        private void LoadChunks()
        {
            for (var x = -loadDistance; x <= loadDistance; x++)
            {
                for (var y = -loadDistance; y <= loadDistance; y++)
                {
                    var chunkPos = new Vector3Int(previousPlayerChunk.x + x, previousPlayerChunk.y + y, 0);
                    if (!loadedChunks.ContainsKey(chunkPos))
                    {
                        var newChunk = new Chunk(ChunkSize, chunkPos, tiles, tilemapLayers[0], tilemapLayers[1], tilemapLayers[2], tilemapLayers[3]);
                        newChunk.Generate();
                        loadedChunks.Add(chunkPos, newChunk);
                    }
                }
            }

            var chunksToRemove = new List<Vector3Int>();

            foreach (var (position, chunk) in loadedChunks)
            {
                if (position.x < previousPlayerChunk.x - unloadDistance || position.x > previousPlayerChunk.x + unloadDistance ||
                    position.y < previousPlayerChunk.y - unloadDistance || position.y > previousPlayerChunk.y + unloadDistance)
                {
                    chunk.Clear();
                    chunksToRemove.Add(position);
                }
            }

            foreach (var removeChunkPos in chunksToRemove)
            {
                loadedChunks.Remove(removeChunkPos);
            }
        }
    }
}