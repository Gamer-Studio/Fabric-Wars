using System;
using UnityEngine;

namespace FabricWars.Utils.Extensions
{
    public static class VectorExtensions
    {
        // Vector3
        public static Vector3 X(this Vector3 position, float x) => new(x, position.y, position.z);
        public static Vector3 Y(this Vector3 position, float y) => new(position.x, y, position.z);
        public static Vector3 Z(this Vector3 position, float z) => new(position.x, position.y, z);
        public static Vector3 Add(this Vector3 position, float x, float y, float z) => new(x + position.x, y + position.y, z + position.z);
        public static Vector3 Multiply(this Vector3 position, float x, float y, float z) => new(x * position.x, y * position.y, z * position.z);
        public static Vector3 Negate(this Vector3 position) => new(-position.x, -position.y, -position.z);
        public static Vector3 Abs(this Vector3 position) => new(Math.Abs(position.x), Math.Abs(position.y), Math.Abs(position.z));
        public static Vector3 Min(this Vector3 position1, Vector3 position2) => new(Math.Min(position1.x, position2.x), Math.Min(position1.y, position2.y), Math.Min(position1.z, position2.z));
        public static Vector3 Max(this Vector3 position1, Vector3 position2) => new(Math.Max(position1.x, position2.x), Math.Max(position1.y, position2.y), Math.Max(position1.z, position2.z));
        public static Vector3 Clamp(this Vector3 position, Vector3 min, Vector3 max) => new(Math.Clamp(position.x, min.x, max.x), Math.Clamp(position.y, min.y, max.y), Math.Clamp(position.z, min.z, max.z));
        public static Vector3 Clamp(this Vector3 position, float min, float max) => new(Math.Clamp(position.x, min, max), Math.Clamp(position.y, min, max), Math.Clamp(position.z, min, max));
        public static Vector3 Lerp(this Vector3 position1, Vector3 position2, float amount) => new(
            position1.x + (position2.x - position1.x) * amount, 
            position1.y + (position2.y - position1.y) * amount, 
            position1.z + (position2.z - position1.z) * amount
            );
        public static Vector3 LerpUnclamped(this Vector3 position1, Vector3 position2, float amount) => new(
            position1.x + (position2.x - position1.x) * amount, 
            position1.y + (position2.y - position1.y) * amount, 
            position1.z + (position2.z - position1.z) * amount
            );

        public static void Deconstruct(this Vector3 position, out float x, out float y, out float z)
        {
            x = position.x;
            y = position.y;
            z = position.z;
        }
        public static void Deconstruct(this Vector3 position, out float x, out float y)
        {
            x = position.x;
            y = position.y;
        }

        // Vector2Int
        public static Vector2Int X(this Vector2Int position, int x) => new(x, position.y);
        public static Vector2Int Y(this Vector2Int position, int y) => new(position.x, y);
        public static Vector2Int Add(this Vector2Int position, int x, int y) => new(x + position.x, y + position.y);
        public static Vector2Int Multiply(this Vector2Int position, int x, int y) => new(x * position.x, y * position.y);
        public static Vector2Int Negate(this Vector2Int position) => new(-position.x, -position.y);
        public static Vector2Int Abs(this Vector2Int position) => new(Math.Abs(position.x), Math.Abs(position.y));
        public static Vector2Int Min(this Vector2Int position1, Vector2Int position2) => new(Math.Min(position1.x, position2.x), Math.Min(position1.y, position2.y));
        public static Vector2Int Max(this Vector2Int position1, Vector2Int position2) => new(Math.Max(position1.x, position2.x), Math.Max(position1.y, position2.y));
        public static Vector2Int Clamp(this Vector2Int position, Vector2Int min, Vector2Int max) => new(Math.Clamp(position.x, min.x, max.x), Math.Clamp(position.y, min.y, max.y));
        public static Vector2Int Clamp(this Vector2Int position, int min, int max) => new(Math.Clamp(position.x, min, max), Math.Clamp(position.y, min, max));
        
        public static Vector2Int Lerp(this Vector2Int position1, Vector2Int position2, float amount) => new(
            (int)(position1.x + (position2.x - position1.x) * amount),
            (int)(position1.y + (position2.y - position1.y) * amount)
            );
        
        public static Vector2Int LerpUnclamped(this Vector2Int position1, Vector2Int position2, float amount) => new(
            (int)(position1.x + (position2.x - position1.x) * amount),
            (int)(position1.y + (position2.y - position1.y) * amount)
            );
        
        public static void Deconstruct(this Vector2Int position, out int x, out int y)
        {
            x = position.x;
            y = position.y;
        }
        
        // Vector3Int
        public static Vector3Int X(this Vector3Int position, int x) => new(x, position.y, position.z);
        public static Vector3Int Y(this Vector3Int position, int y) => new(position.x, y, position.z);
        public static Vector3Int Z(this Vector3Int position, int z) => new(position.x, position.y, z);
        public static Vector3Int Add(this Vector3Int position, int x, int y, int z) => new(x + position.x, y + position.y, position.z + z);
        public static Vector3Int Multiply(this Vector3Int position, int x, int y, int z) => new(x * position.x, y * position.y, z * position.z);
        public static Vector3Int Negate(this Vector3Int position) => new(-position.x, -position.y, -position.z);
        public static Vector3Int Abs(this Vector3Int position) => new(Math.Abs(position.x), Math.Abs(position.y), Math.Abs(position.z));
        public static Vector3Int Min(this Vector3Int position1, Vector3Int position2) => new(Math.Min(position1.x, position2.x), Math.Min(position1.y, position2.y), Math.Min(position1.z, position2.z));
        public static Vector3Int Max(this Vector3Int position1, Vector3Int position2) => new(Math.Max(position1.x, position2.x), Math.Max(position1.y, position2.y), Math.Max(position1.z, position2.z));
        public static Vector3Int Clamp(this Vector3Int position, Vector3Int min, Vector3Int max) => new(Math.Clamp(position.x, min.x, max.x), Math.Clamp(position.y, min.y, max.y), Math.Clamp(position.z, min.z, max.z));
        public static Vector3Int Clamp(this Vector3Int position, int min, int max) => new(Math.Clamp(position.x, min, max), Math.Clamp(position.y, min, max), Math.Clamp(position.z, min, max));

        public static Vector3Int Lerp(this Vector3Int position1, Vector3Int position2, float amount) => new(
            (int)(position1.x + (position2.x - position1.x) * amount),
            (int)(position1.y + (position2.y - position1.y) * amount),
            (int)(position1.z + (position2.z - position1.z) * amount)
        );

        public static Vector3Int LerpUnclamped(this Vector3Int position1, Vector3Int position2, float amount) => new(
            (int)(position1.x + (position2.x - position1.x) * amount),
            (int)(position1.y + (position2.y - position1.y) * amount),
            (int)(position1.z + (position2.z - position1.z) * amount)
        );
        
        public static void Deconstruct(this Vector3Int position, out int x, out int y, out int z)
        {
            x = position.x;
            y = position.y;
            z = position.z;
        }
        public static void Deconstruct(this Vector3Int position, out int x, out int y)
        {
            x = position.x;
            y = position.y;
        }
        
        // 
        public static Vector2Int ToVector2Int(this Vector3Int vector) => new(vector.x, vector.y);
        public static Vector3Int ToVector3Int(this Vector2Int vector) => new(vector.x, vector.y, 0);
        public static Vector3Int ToVector3Int(this Vector2Int vector, int z) => new(vector.x, vector.y, z);
        
        public static Vector2 ToVector2(this Vector3 vector) => new(vector.x, vector.y);
        public static Vector3 ToVector3(this Vector2 vector) => new(vector.x, vector.y, 0);
        public static Vector3 ToVector3(this Vector2 vector, int z) => new(vector.x, vector.y, z);
        
        public static Vector2 ToVector2(this Vector2Int vector) => new(vector.x, vector.y);
        public static Vector2Int ToVector2Int(this Vector2 vector) => new((int)vector.x, (int)vector.y);
        
        public static Vector3 ToVector3(this Vector3Int vector) => new(vector.x, vector.y, vector.z);
        public static Vector3Int ToVector3Int(this Vector3 vector) => new((int)vector.x, (int)vector.y, (int)vector.z);
    }
}