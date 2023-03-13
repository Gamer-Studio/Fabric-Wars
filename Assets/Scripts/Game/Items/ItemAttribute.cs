using System;
using UnityEngine;

namespace FabricWars.Game.Items
{
    [Flags]
    public enum ItemAttribute
    {
        Causality = 1 << 0,
        Life = 1 << 1,
        Water = 1 << 2,
        Fire = 1 << 3,
        None = 0
    }

    public static class ItemAttributeManager
    {
        public static Color GetColor(this ItemAttribute attribute) => attribute switch
        {
            ItemAttribute.Causality => Color.magenta,
            ItemAttribute.Life => Color.green,
            ItemAttribute.Water => Color.blue,
            ItemAttribute.Fire => new Color(1f, 0.5f, 0f),
            _ => Color.white
        };
    }
}