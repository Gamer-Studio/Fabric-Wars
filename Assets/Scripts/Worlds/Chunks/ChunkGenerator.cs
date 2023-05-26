using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds.Chunks
{
    [CreateAssetMenu(fileName = "new Chunk Generator", menuName = "World/Chunk/Basic Chunk Generator")]
    public class ChunkGenerator : ScriptableObject
    {
        public TileBase tile;

        public virtual Chunk Generate(World world, Vector3Int position)
        {
            Debug.Log(position);
            var chunkSize = world.chunkSize;
            var layers = world.tilemapLayers;
            var resultChunk = new Chunk(world, position);

            layers[1].SetTilesBlock(new BoundsInt(position * chunkSize, new Vector3Int(chunkSize, chunkSize, 1)),
                Enumerable.Repeat(tile, chunkSize * chunkSize).ToArray());

            return resultChunk;
        }
    }
}