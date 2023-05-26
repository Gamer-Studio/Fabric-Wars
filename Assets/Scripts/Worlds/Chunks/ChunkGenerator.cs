using UnityEngine;

namespace FabricWars.Worlds.Chunks
{
    public abstract class ChunkGenerator : ScriptableObject
    {

        public virtual Chunk Generate(World world, Vector3Int position)
        {
            var resultChunk = new Chunk(world, position);
            return resultChunk;
        }
    }
}