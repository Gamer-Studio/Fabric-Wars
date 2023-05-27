using System.Linq;
using FabricWars.Worlds.Tiles;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds.Chunks
{
    [CreateAssetMenu(fileName = "new Grassland Generator", menuName = "World/Chunk/Grassland")]
    public class GrasslandGenerator : ChunkGenerator
    {
        public WorldTile baseTile;
        public override Chunk Generate(World world, Vector3Int position)
        {
            var (chunk, layers, chunkSize) = base.Generate(world, position);
            
            layers[1].SetTilesBlock(new BoundsInt(position * chunkSize, new Vector3Int(chunkSize, chunkSize, 1)),
                Enumerable.Repeat<TileBase>(baseTile, chunkSize * chunkSize).ToArray());
            
            
            return chunk;
        }
    }
}