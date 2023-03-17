using UnityEngine;

namespace FabricWars.Utils.KeyBinds
{
    public static class KeyCodeUtils
    {
        public static KeyCode[]
            Numberics =
            {
                KeyCode.Alpha0,
                KeyCode.Alpha1,
                KeyCode.Alpha2,
                KeyCode.Alpha3,
                KeyCode.Alpha4,
                KeyCode.Alpha5,
                KeyCode.Alpha6,
                KeyCode.Alpha7,
                KeyCode.Alpha8,
                KeyCode.Alpha9,
            },
            Horizontal =
            {
                KeyCode.A,
                KeyCode.D,
                KeyCode.LeftArrow,
                KeyCode.RightArrow
            },
            Vertical =
            {
                KeyCode.W,
                KeyCode.S,
                KeyCode.UpArrow,
                KeyCode.DownArrow
            };

        public static bool TryToInt(KeyCode code, out int value)
        {
            if (code is >= KeyCode.Alpha0 and <= KeyCode.Alpha9)
            {
                value = code - KeyCode.Alpha0;
                return true;
            }

            value = -1;
            return false;
        }
    }
}