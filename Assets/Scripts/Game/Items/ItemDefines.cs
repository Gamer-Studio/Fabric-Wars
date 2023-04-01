using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Items
{
    public partial class Item
    {
        public static readonly Dictionary<string, Item> allocated = new();
        
        public static Item None, Log, Coin, Earth;
        
        internal static void Load()
        {
            Addressables.LoadAssetsAsync<Item>(new AssetLabelReference { labelString = "Item" },
                item => allocated[item.name] = item).WaitForCompletion();

            allocated.TryGetValue("None", out None);
            allocated.TryGetValue("Log", out Log);
            allocated.TryGetValue("Coin", out Coin);
            allocated.TryGetValue("Earth", out Earth);

            Debug.Log($"{allocated.Count} items loaded");
        }
    }
}