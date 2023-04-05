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
                .LoadAssetsAsync<BulletComponent>(new AssetLabelReference { labelString = "BulletComponent" },
                    comp => allocated[comp.name] = comp).WaitForCompletion();
            
            Debug.Log(allocated.Count + " bullet components loaded");
        }
    }
}