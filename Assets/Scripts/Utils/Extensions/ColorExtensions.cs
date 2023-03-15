using System;
using UnityEngine;

namespace FabricWars.Utils.Extensions
{
    public static class ColorExtensions
    {
        private const float Epsilon = 0.00001f;

        public static Color Edit(this Color color, float r = -1, float g = -1, float b = -1, float a = -1)
        {
            return new Color(
                Math.Abs(r + 1) < Epsilon ? color.r : r,
                Math.Abs(g + 1) < Epsilon ? color.g : g,
                Math.Abs(b + 1) < Epsilon ? color.b : b,
                Math.Abs(a + 1) < Epsilon ? color.a : a
            );
        }

        public static Color R(this Color color, Func<float, float> r)
        {
            var newR = r(color.r);
            return new Color(color.r, color.g, color.b, color.a);
        }

        public static Color R(this Color color, float r) => new Color(r, color.g, color.b, color.a);

        public static Color G(this Color color, Func<float, float> g)
        {
            var newG = g(color.g);
            return new Color(color.r, newG, color.b, color.a);
        }

        public static Color G(this Color color, float g) => new Color(color.r, g, color.b, color.a);

        public static Color B(this Color color, Func<float, float> b)
        {
            var newB = b(color.b);
            return new Color(color.r, color.g, newB, color.a);
        }

        public static Color B(this Color color, float b) => new Color(color.r, color.g, b, color.a);

        public static Color A(this Color color, Func<float, float> a)
        {
            var newA = a(color.a);
            return new Color(color.r, color.g, color.b, newA);
        }

        public static Color A(this Color color, float a) => new Color(color.r, color.g, color.b, a);
    }
}