using UnityEngine;

namespace FabricWars.Worlds.Chunks
{
    [CreateAssetMenu(fileName = "new Grassland Generator", menuName = "World/Chunk/Grassland")]
    public class GrasslandGenerator : ChunkGenerator
    {
        public override Chunk Generate(World world, Vector3Int position)
        {
            var (chunk, layers, chunkSize) = base.Generate(world, position);

            return chunk;
        }
    }
}