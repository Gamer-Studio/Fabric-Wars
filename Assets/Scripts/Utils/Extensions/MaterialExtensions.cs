using System;
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

        /* TODO
         * configs 의 object값을 switch로 분리한뒤에 foreach 통해서 int 키 값에 따라 통합적으로 변경
         */
        public static Material SetConfig(this Material material, params SerializablePair<int, object>[] configs)
        {
            return material;
        }
    }
}