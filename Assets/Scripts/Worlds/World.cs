using System.Collections;
using System.Collections.Generic;
using FabricWars.Utils.Serialization;
using FabricWars.Worlds.Chunks;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds
{
    public class World : MonoBehaviour
    {
        [Header("ChunkLoader")] 
        [SerializeField] private int _chunkSize = 10;
        public int chunkSize => _chunkSize;
        public ChunkGenerator baseGenerator;
        public bool isInfinite = true;
        
        public List<Tilemap> tilemapLayers;
        public int loadDistance;
        public int unloadDistance;
        public TileBase[] tiles;

        [SerializeField] private Vector3Int previousPlayerChunk;
        [SerializeField] private Transform playerTransform;
        [SerializeField] private SerializableDictionary<Vector3Int, Chunk> loadedChunks = new();

        private void Start()
        {
            playerTransform = Camera.main.transform; // 이건 null 일 수가 있나....?
            previousPlayerChunk = WorldToChunkPosition(playerTransform.position);
            LoadChunks();
            StartCoroutine(TimeUpdater());
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

        private Vector3Int WorldToChunkPosition(Vector3 position)
        {
            var targetPos = position - transform.position;
            return new Vector3Int(Mathf.FloorToInt(targetPos.x / chunkSize), Mathf.FloorToInt(targetPos.y / chunkSize),
                0);
        }

        private void LoadChunks()
        {
            for (var x = -loadDistance; x <= loadDistance; x++)
            {
                for (var y = -loadDistance; y <= loadDistance; y++)
                {
                    var chunkPos = new Vector3Int(previousPlayerChunk.x + x, previousPlayerChunk.y + y, 0);
                    if (!loadedChunks.ContainsKey(chunkPos) && isInfinite)
                    {
                        var newChunk = baseGenerator.Generate(this, chunkPos); //new Chunk(this, chunkPos, tiles);
                        loadedChunks.Add(chunkPos, newChunk);
                    }
                }
            }

            var chunksToRemove = new List<Vector3Int>();

            foreach (var (position, chunk) in loadedChunks)
            {
                if (position.x < previousPlayerChunk.x - unloadDistance ||
                    position.x > previousPlayerChunk.x + unloadDistance ||
                    position.y < previousPlayerChunk.y - unloadDistance ||
                    position.y > previousPlayerChunk.y + unloadDistance)
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

        [Header("Time System")] public UnityEvent onDayPass;

        public Light2D globalLight;

        private const int HalfDay = 8; //  분 단위
        public (float max, float min) brightness = (0.6f, 0.05f);
        [SerializeField] private int _currentTime = 0;

        private IEnumerator TimeUpdater()
        {
            var brightnessChange = (brightness.max - brightness.min) / HalfDay;

            while (true)
            {
                _currentTime++;
                
                if (_currentTime > HalfDay * 2)
                {
                    _currentTime = 0;
                    onDayPass.Invoke();
                    continue;
                }
                
                globalLight.intensity = brightness.min + brightnessChange *
                    (_currentTime < HalfDay ? _currentTime : HalfDay * 2 - _currentTime);

                yield return new WaitForSeconds(60);
            }
        }
    }
}