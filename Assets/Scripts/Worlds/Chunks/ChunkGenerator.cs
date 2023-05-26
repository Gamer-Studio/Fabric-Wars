using FabricWars.Utils.Extensions;
using FabricWars.Worlds.Tiles;
using UnityEngine;

namespace FabricWars.Worlds.Chunks
{
    [CreateAssetMenu(fileName = "new Chunk Generator", menuName = "World/Chunk/Basic Chunk Generator")]
    public class ChunkGenerator : ScriptableObject
    {
        public WorldTile tile;

        public virtual Chunk Generate(World world, Vector3Int position)
        {
            var chunkSize = world.chunkSize;
            var layers = world.tilemapLayers;
            var resultChunk = new Chunk(world, position);

            var startPos = new Vector3Int(position.x * chunkSize, position.y * chunkSize);
            layers[1].SetTiles(startPos, startPos.Add(chunkSize - 1, chunkSize - 1, 0), tile);

            return resultChunk;
        }
    }
}