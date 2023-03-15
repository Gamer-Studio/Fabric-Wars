using System;
using FabricWars.Utils.Extensions;
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
        Gold = 1 << 4,
        None = 0
    }

    public static class ItemAttributeManager
    {
        public static Color GetColor(this ItemAttribute attribute) => attribute switch
        {
            ItemAttribute.None => Color.white.A(100 / 255f),
            ItemAttribute.Causality => Color.magenta.A(100 / 255f),
            ItemAttribute.Life => Color.green.A(100 / 255f),
            ItemAttribute.Water => Color.blue.A(100 / 255f),
            ItemAttribute.Fire => new Color(140 / 255f, 23 / 255f, 0f).A(100 / 255f),
            ItemAttribute.Gold => Color.yellow.A(100 / 255f),
            _ => Color.white
        };
    }
}