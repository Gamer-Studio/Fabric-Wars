using System;

namespace FabricWars.Utils
{
    public static class MathEx
    {
        public static int Range(this int value, int max, int min) => Math.Clamp(value, max, min);

        public static int Range(this int value, int max, int min, out int overOrLack)
        {
            if (value > max)
            {
                overOrLack = value - max;
                return max;
            }
            
            if (value < min)
            {
                overOrLack = min - value;
                return min;
            }

            {
                overOrLack = 0;
                return value;
            }
        }
        
        public static int Max(this int value, int max, out int over)
        {
            if (value < max)
            {
                over = 0;
                return value;
            }
            else
            {
                over = value - max;
                return max;
            }
        }

        public static int Min(this int value, int min, out int lack)
        {
            if (value > min)
            {
                lack = 0;
                return value;
            }
            else
            {
                lack = min - value;
                return min;
            }
        }
    }
}