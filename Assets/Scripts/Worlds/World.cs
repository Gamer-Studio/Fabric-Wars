using System.Collections.Generic;
using FabricWars.Utils.Serialization;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds
{
    public class World : MonoBehaviour
    {
        public const int ChunkSize = 20;
        public int viewDistance;
        public Tilemap tilemap;
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
            var newChunks = new HashSet<Vector3Int>();

            for (var x = -viewDistance; x <= viewDistance; x++)
            {
                for (var y = -viewDistance; y <= viewDistance; y++)
                {
                    var chunkPos = new Vector3Int(previousPlayerChunk.x + x, previousPlayerChunk.y + y, 0);
                    if (!loadedChunks.ContainsKey(chunkPos))
                    {
                        var newChunk = new Chunk(ChunkSize, chunkPos, tilemap, tiles);
                        loadedChunks.Add(chunkPos, newChunk);
                    }

                    newChunks.Add(chunkPos);
                }
            }

            var chunksToRemove = new List<Vector3Int>();

            foreach (var (position, chunk) in loadedChunks)
            {
                if (!newChunks.Contains(position))
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