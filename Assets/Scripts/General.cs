﻿using System.Collections.Generic;
using FabricWars.Game.Items;
using UnityEngine.AddressableAssets;

namespace FabricWars
{
    public static class General
    {
        public static bool inited { get; private set; }
        public static readonly Dictionary<string, Item> items = new();

        public static void Initialization()
        {
            if(inited) return;
            
            Addressables.LoadAssetsAsync<Item>(new AssetLabelReference { labelString = "ItemSO" }, item =>
            {
                items[item.name] = item;
            }).WaitForCompletion();
            
            Item.InitItems();
            
            inited = true;
        }
    }
}