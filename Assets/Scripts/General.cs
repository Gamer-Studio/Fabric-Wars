using System.Collections.Generic;
using FabricWars.Game.Items;
using UnityEngine.AddressableAssets;

namespace FabricWars
{
    public static class General
    {
        public static bool inited { get; private set; }
        public static readonly Dictionary<string, ItemAttribute> itemAttributes = new();
        public static readonly Dictionary<string, Item> items = new();

        public static void Initialization()
        {
            if (inited) return;

            Addressables.LoadAssetsAsync<ItemAttribute>(new AssetLabelReference { labelString = "ItemAttributeSO" },
                attr => itemAttributes[attr.name] = attr).WaitForCompletion();
            
            ItemAttribute.Init();
            
            Addressables.LoadAssetsAsync<Item>(new AssetLabelReference { labelString = "ItemSO" },
                item => items[item.name] = item).WaitForCompletion();

            Item.Init();

            inited = true;
        }
    }
}