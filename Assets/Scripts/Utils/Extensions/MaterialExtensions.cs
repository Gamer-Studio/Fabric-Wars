using System;
using System.Collections.Generic;
using FabricWars.Utils.Serialization;
using UnityEngine;

namespace FabricWars.Utils.Extensions
{
    public static class MaterialExtensions
    {
        public static bool GetBool(this Material material, int nameID)
        {
            var val = material.GetInteger(nameID);
            if (val != 0 && val != 1) throw new Exception();
            return val != 0;
        }

        public static bool GetBool(this Material material, string name)
        {
            var val = material.GetInteger(name);
            if (val != 0 && val != 1) throw new Exception();
            return val != 0;
        }

        public static bool HasBool(this Material material, int nameID) => material.HasInteger(nameID);
        public static bool HasBool(this Material material, string name) => material.HasInteger(name);

        public static void SetBool(this Material material, int nameID, bool value) =>
            material.SetInt(nameID, value ? 1 : 0);

        public static void SetBool(this Material material, string name, bool value) =>
            material.SetInt(name, value ? 1 : 0);

        public static Material SetConfig(this Material material, params SerializablePair<int, object>[] configs)
        {
            foreach((int id, object obj) in configs)
            {
                Action callback = obj switch
                {
                    bool => () => material.SetBool(id, (bool)obj),
                    ComputeBuffer => () => material.SetBuffer(id, (ComputeBuffer)obj),
                    Color => () => material.SetColor(id, (Color)obj),
                    List<Color> => () => material.SetColorArray(id, (List<Color>)obj),
                    float => () => material.SetFloat(id, (float)obj),
                    List<float> => () => material.SetFloatArray(id, (List<float>)obj),
                    int => () => material.SetInteger(id, (int)obj),
                    Matrix4x4 => () => material.SetMatrix(id, (Matrix4x4)obj),
                    List<Matrix4x4> => () => material.SetMatrixArray(id, (List<Matrix4x4>)obj),
                    Texture => () => material.SetTexture(id, (Texture)obj), // SetTextureOffset와 Scale는 어떻게 처리함?
                    Vector4 => () => material.SetVector(id, (Vector4)obj),
                    List<Vector4> => () => material.SetVectorArray(id, (List<Vector4>)obj),
                    _ => throw new NotImplementedException()
                };
                callback();
            }

            return material;
        }
    }
}