using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Bullets
{
    public partial class BulletComponent
    {
        public static readonly Dictionary<string, BulletComponent> allocated = new();

        public static void Load()
        {
            Addressables
                .LoadAssetsAsync<BulletComponent>(new AssetLabelReference { labelString = "" },
                    comp => allocated[comp.name] = comp).WaitForCompletion();
            
            Debug.Log("Loaded " + allocated.Count + " bullet components");
        }
    }
}