using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds.Chunks
{
    [CreateAssetMenu(fileName = "new Basic Generator", menuName = "World/Chunk/Basic")]
    public class BasicChunkGenerator : ChunkGenerator
    {
        public TileBase tile;

        public override Chunk Generate(World world, Vector3Int position)
        {
            var (chunk, layers, chunkSize) = base.Generate(world, position);

            layers[1].SetTilesBlock(new BoundsInt(position * chunkSize, new Vector3Int(chunkSize, chunkSize, 1)),
                Enumerable.Repeat(tile, chunkSize * chunkSize).ToArray());
            
            return chunk;
        }
    }
}