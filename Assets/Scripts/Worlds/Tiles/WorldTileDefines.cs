using System.Collections.Generic;

namespace FabricWars.Worlds.Tiles
{
    public partial class WorldTile
    {
        public static readonly Dictionary<string, WorldTile> allocated = new();

        public static WorldTile None;

        public static void Load()
        {
            allocated.TryGetValue("Tile_None", out None);
        }
    }
}