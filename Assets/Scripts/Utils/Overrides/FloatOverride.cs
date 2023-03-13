using System;

namespace FabricWars.Utils.Overrides
{
    public static class FloatOverride
    {
        public static float tolerance = 0.1f;
        
        public static bool FEqual(this float origin, float target)
        {
            return Math.Abs(origin - target) < tolerance;
        }
    }
}