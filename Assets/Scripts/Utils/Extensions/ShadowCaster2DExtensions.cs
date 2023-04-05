using System.Reflection;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace FabricWars.Utils.Extensions
{
    public static class ShadowCaster2DExtensions
    {
        public static void SetShapePath(this ShadowCaster2D shadow, params Vector3[] path)
        {
            var m_ShapePath = typeof(ShadowCaster2D).GetField("m_ShapePath", BindingFlags.Instance | BindingFlags.NonPublic);
            var m_ShapePathHash = typeof(ShadowCaster2D).GetField("m_ShapePathHash", BindingFlags.Instance | BindingFlags.NonPublic);
            
            if(m_ShapePath == null || m_ShapePathHash == null) return;
            
            m_ShapePath.SetValue(shadow, path);

            int hashCode;
            
            unchecked
            {
                var hash = (int)2166136261;

                if (path != null)
                {
                    foreach (var point in path)
                        hash = hash * 16777619 ^ point.GetHashCode();
                }
                else
                {
                    hash = 0;
                }

                hashCode = hash;
            }
            
            m_ShapePathHash.SetValue(shadow, hashCode);
        }
    }
}