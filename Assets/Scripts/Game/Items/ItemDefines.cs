using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Items
{
    public partial class Item
    {
        private static bool _loaded = false;
        
        public static readonly Dictionary<string, Item> allocated = new();
        
        public static Item None, Log, Coin;
        
        public static void Load()
        {
            if (_loaded) return;
            
            Addressables.LoadAssetsAsync<Item>(new AssetLabelReference { labelString = "ItemSO" },
                item => allocated[item.name] = item).WaitForCompletion();

            allocated.TryGetValue("None", out None);
            allocated.TryGetValue("Log", out Log);
            allocated.TryGetValue("Coin", out Coin);

            Debug.Log($"{allocated.Count} items loaded");
            
            _loaded = true;
        }
    }
}