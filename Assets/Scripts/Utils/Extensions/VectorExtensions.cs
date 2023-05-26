using System;
using System.Collections.Generic;
using UnityEngine;

namespace FabricWars.Utils.Extensions
{
    public static class VectorExtensions
    {
        #region Vector2

        public static Vector2 X(this Vector2 vector, float x) => new(x, vector.y);
        public static Vector2 Y(this Vector2 vector, float y) => new(vector.x, y);
        public static void Alloc(this Vector2 vector, out Vector2 target) => target = vector;
        public static Vector2 Add(this Vector2 vector, float x, float y) => new(vector.x + x, vector.y + y);

        public static Vector2 Add(this Vector2 vector1, Vector2 vector2) =>
            new(vector1.x + vector2.x, vector1.y + vector2.y);

        public static Vector2 Multiply(this Vector2 vector, float x, float y) => new(vector.x * x, vector.y * y);
        public static Vector2 Abs(this Vector2 vector) => new(Math.Abs(vector.x), Math.Abs(vector.y));

        public static Vector2 Min(this Vector2 vector1, Vector2 vector2) =>
            new(Math.Min(vector1.x, vector2.x), Math.Min(vector1.y, vector2.y));

        public static Vector2 Max(this Vector2 vector1, Vector2 vector2) =>
            new(Math.Max(vector1.x, vector2.x), Math.Max(vector1.y, vector2.y));

        public static Vector2 Action(this Vector2 vector, Func<Vector2, Vector2> action) => action(vector);
        public static T Action<T>(this Vector2 vector, Func<Vector2, T> action) => action(vector);

        #endregion Vector2

        #region Vector3

        public static Vector3 X(this Vector3 vector, float x) => new(x, vector.y, vector.z);
        public static Vector3 Y(this Vector3 vector, float y) => new(vector.x, y, vector.z);
        public static Vector3 Z(this Vector3 vector, float z) => new(vector.x, vector.y, z);
        public static Vector3 XY(this Vector3 vector, float x, float y) => new(x, y, vector.z);
        public static Vector3 XZ(this Vector3 vector, float x, float z) => new(x, vector.y, z);
        public static Vector3 YZ(this Vector3 vector, float y, float z) => new(vector.x, y, z);

        public static Vector3 Add(this Vector3 vector, float x, float y, float z) =>
            new(x + vector.x, y + vector.y, z + vector.z);

        public static Vector3 Multiply(this Vector3 vector, float x, float y, float z) =>
            new(x * vector.x, y * vector.y, z * vector.z);

        public static Vector3 Abs(this Vector3 vector) =>
            new(Math.Abs(vector.x), Math.Abs(vector.y), Math.Abs(vector.z));

        public static Vector3 Min(this Vector3 vector1, Vector3 vector2) => new(Math.Min(vector1.x, vector2.x),
            Math.Min(vector1.y, vector2.y), Math.Min(vector1.z, vector2.z));

        public static Vector3 Max(this Vector3 vector1, Vector3 vector2) => new(Math.Max(vector1.x, vector2.x),
            Math.Max(vector1.y, vector2.y), Math.Max(vector1.z, vector2.z));

        public static Vector3 Clamp(this Vector3 vector, Vector3 min, Vector3 max) => new(
            Math.Clamp(vector.x, min.x, max.x), Math.Clamp(vector.y, min.y, max.y), Math.Clamp(vector.z, min.z, max.z));

        public static Vector3 Clamp(this Vector3 vector, float min, float max) => new(Math.Clamp(vector.x, min, max),
            Math.Clamp(vector.y, min, max), Math.Clamp(vector.z, min, max));

        public static Vector3 Lerp(this Vector3 vector1, Vector3 vector2, float amount) => new(
            vector1.x + (vector2.x - vector1.x) * amount,
            vector1.y + (vector2.y - vector1.y) * amount,
            vector1.z + (vector2.z - vector1.z) * amount
        );

        public static Vector3 LerpUnclamped(this Vector3 vector1, Vector3 vector2, float amount) => new(
            vector1.x + (vector2.x - vector1.x) * amount,
            vector1.y + (vector2.y - vector1.y) * amount,
            vector1.z + (vector2.z - vector1.z) * amount
        );

        public static Vector3 Vector3Distance(this Vector3 vector, Vector3 target) => new(
            target.x - vector.x,
            target.y - vector.y,
            target.z - vector.z
        );

        public static void Deconstruct(this Vector3 vector, out float x, out float y, out float z)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public static void Deconstruct(this Vector3 vector, out float x, out float y)
        {
            x = vector.x;
            y = vector.y;
        }

        #endregion Vector3

        #region Vector2Int

        public static Vector2Int X(this Vector2Int vector, int x) => new Vector2Int(x, vector.y);
        public static Vector2Int Y(this Vector2Int vector, int y) => new Vector2Int(vector.x, y);
        public static void Alloc(this Vector2Int vector, out Vector2Int target) => target = vector;

        public static Vector2Int Add(this Vector2Int vector, int x, int y) =>
            new Vector2Int(x + vector.x, y + vector.y);

        public static Vector2Int Multiply(this Vector2Int vector, int x, int y) =>
            new Vector2Int(x * vector.x, y * vector.y);

        public static Vector2Int Abs(this Vector2Int vector) => new Vector2Int(Math.Abs(vector.x), Math.Abs(vector.y));

        public static Vector2Int Min(this Vector2Int vector1, Vector2Int vector2) =>
            new Vector2Int(Math.Min(vector1.x, vector2.x), Math.Min(vector1.y, vector2.y));

        public static Vector2Int Max(this Vector2Int vector1, Vector2Int vector2) =>
            new Vector2Int(Math.Max(vector1.x, vector2.x), Math.Max(vector1.y, vector2.y));

        public static Vector2Int Clamp(this Vector2Int vector, Vector2Int min, Vector2Int max) =>
            new Vector2Int(Math.Clamp(vector.x, min.x, max.x), Math.Clamp(vector.y, min.y, max.y));

        public static Vector2Int Clamp(this Vector2Int vector, int min, int max) =>
            new Vector2Int(Math.Clamp(vector.x, min, max), Math.Clamp(vector.y, min, max));

        public static Vector2Int Lerp(this Vector2Int vector1, Vector2Int vector2, float amount) => new Vector2Int(
            (int)(vector1.x + (vector2.x - vector1.x) * amount),
            (int)(vector1.y + (vector2.y - vector1.y) * amount)
        );

        public static Vector2Int LerpUnclamped(this Vector2Int vector1, Vector2Int vector2, float amount) =>
            new Vector2Int(
                (int)(vector1.x + (vector2.x - vector1.x) * amount),
                (int)(vector1.y + (vector2.y - vector1.y) * amount)
            );

        public static void Deconstruct(this Vector2Int vector, out int x, out int y)
        {
            x = vector.x;
            y = vector.y;
        }

        #endregion Vector2Int

        #region Vector3Int

        public static Vector3Int X(this Vector3Int vector, int x) => new(x, vector.y, vector.z);
        public static Vector3Int Y(this Vector3Int vector, int y) => new(vector.x, y, vector.z);
        public static Vector3Int Z(this Vector3Int vector, int z) => new(vector.x, vector.y, z);
        public static Vector3Int XY(this Vector3Int vector, int x, int y) => new(x, y, vector.z);

        public static Vector3Int Add(this Vector3Int vector, int x, int y, int z) =>
            new(x + vector.x, y + vector.y, vector.z + z);

        public static Vector3Int Multiply(this Vector3Int vector, int x, int y, int z) =>
            new(x * vector.x, y * vector.y, z * vector.z);

        public static Vector3Int Abs(this Vector3Int vector) =>
            new(Math.Abs(vector.x), Math.Abs(vector.y), Math.Abs(vector.z));

        public static Vector3Int Min(this Vector3Int vector1, Vector3Int vector2) => new(Math.Min(vector1.x, vector2.x),
            Math.Min(vector1.y, vector2.y), Math.Min(vector1.z, vector2.z));

        public static Vector3Int Max(this Vector3Int vector1, Vector3Int vector2) => new(Math.Max(vector1.x, vector2.x),
            Math.Max(vector1.y, vector2.y), Math.Max(vector1.z, vector2.z));

        public static Vector3Int Clamp(this Vector3Int vector, Vector3Int min, Vector3Int max) => new(
            Math.Clamp(vector.x, min.x, max.x), Math.Clamp(vector.y, min.y, max.y), Math.Clamp(vector.z, min.z, max.z));

        public static Vector3Int Clamp(this Vector3Int vector, int min, int max) => new(Math.Clamp(vector.x, min, max),
            Math.Clamp(vector.y, min, max), Math.Clamp(vector.z, min, max));

        public static Vector3Int Lerp(this Vector3Int vector1, Vector3Int vector2, float amount) => new(
            (int)(vector1.x + (vector2.x - vector1.x) * amount),
            (int)(vector1.y + (vector2.y - vector1.y) * amount),
            (int)(vector1.z + (vector2.z - vector1.z) * amount)
        );

        public static Vector3Int LerpUnclamped(this Vector3Int vector1, Vector3Int vector2, float amount) => new(
            (int)(vector1.x + (vector2.x - vector1.x) * amount),
            (int)(vector1.y + (vector2.y - vector1.y) * amount),
            (int)(vector1.z + (vector2.z - vector1.z) * amount)
        );

        public static void Deconstruct(this Vector3Int vector, out int x, out int y, out int z)
        {
            x = vector.x;
            y = vector.y;
            z = vector.z;
        }

        public static void Deconstruct(this Vector3Int vector, out int x, out int y)
        {
            x = vector.x;
            y = vector.y;
        }

        public static Vector3Int[] Range(Vector3Int start, Vector3Int end)
        {
            Vector3Int startPos = start.Min(end), endPos = startPos.Max(end);
            var result = new Vector3Int[(endPos.x - startPos.x) * (endPos.y - startPos.y) * (endPos.z - startPos.z)];
            var index = 0;
            
            for (var x = startPos.x; x < endPos.x; x++)
            {
                for (var y = startPos.y; y < endPos.y; y++)
                {
                    if (startPos.z != 0 || endPos.z != 0)
                    {
                        for (var z = startPos.z; z < endPos.z; z++)
                        {
                            result[index] = new Vector3Int(x, y, z);
                            index++;
                        }
                    }
                    else
                    {
                        result[index] = new Vector3Int(x, y, startPos.z);
                        index++;
                    }
                }
            }

            return result;
        }

        #endregion Vector3Int

        #region trans

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

        #endregion trans
    }
}