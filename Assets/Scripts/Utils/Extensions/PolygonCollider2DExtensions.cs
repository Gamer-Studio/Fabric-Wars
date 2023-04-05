using UnityEngine;

namespace FabricWars.Utils.Extensions
{
    public static class PolygonCollider2DExtensions
    {
        public static void ClearPath(this PolygonCollider2D collider)
        {
            collider.pathCount = 0;
        }
    }
}