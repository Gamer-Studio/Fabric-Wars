using UnityEngine;

namespace FabricWars.Utils.Extensions
{
    public static class PolygonCollider2DExtensions
    {
        public static void ClearPath(this PolygonCollider2D collider)
        {
            for (var i = 0; i < collider.pathCount; i++)
            {
                collider.SetPath(i, (Vector2[])null);
            }
        }
    }
}