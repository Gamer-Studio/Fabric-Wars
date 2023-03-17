using System.Collections.Generic;
using FabricWars.Game.Items;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Element = FabricWars.Game.Elements.Element;

namespace FabricWars
{
    public static class General
    {
        public static bool inited { get; private set; }
        public static readonly Dictionary<string, Element> elements = new();
        public static readonly Dictionary<string, Item> items = new();

        public static void Initialization()
        {
            if (inited) return;

            Addressables.LoadAssetsAsync<Element>(new AssetLabelReference { labelString = "ItemAttributeSO" },
                attr => elements[attr.name] = attr).WaitForCompletion();
            
            Element.Init();
            Debug.Log("Loaded " + elements.Count + " item attributes");

            Addressables.LoadAssetsAsync<Item>(new AssetLabelReference { labelString = "ItemSO" },
                item => items[item.name] = item).WaitForCompletion();

            Item.Init();

            inited = true;
        }
    }
}