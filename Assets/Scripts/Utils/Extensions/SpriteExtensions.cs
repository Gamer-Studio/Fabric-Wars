using System.Collections.Generic;
using UnityEngine;

namespace FabricWars.Utils.Extensions
{
    public static class SpriteExtensions
    {
        public static List<Vector2> GetPhysicsShape(this Sprite sprite, int shapeIdx)
        {
            var shape = new List<Vector2>();
            sprite.GetPhysicsShape(shapeIdx, shape);
            
            return shape;
        }
    }
}