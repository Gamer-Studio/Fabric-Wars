using UnityEngine;
using UnityEngine.Tilemaps;

namespace FabricWars.Worlds.Tiles
{
    [CreateAssetMenu]
    public partial class WorldTile : RuleTile<WorldTile.Neighbor>
    {
        public float slowness = 0;

        public class Neighbor : RuleTile.TilingRuleOutput.Neighbor
        {
            public const int Null = 3;
            public const int NotNull = 4;
        }

        public override bool RuleMatch(int neighbor, TileBase tile) => neighbor switch
        {
            Neighbor.Null => tile == null,
            Neighbor.NotNull => tile != null,
            _ => base.RuleMatch(neighbor, tile)
        };
    }
}