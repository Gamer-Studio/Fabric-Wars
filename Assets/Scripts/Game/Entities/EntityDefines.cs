using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Entities
{
    public partial class Entity
    {
        public readonly static Dictionary<string, GameObject> allocated = new ();
        
        public static void Load()
        {
            Addressables.LoadAssetsAsync<GameObject>(new AssetLabelReference{labelString = "EntityPrefab"}, entity =>
            {
                if (allocated.ContainsKey(entity.name)) return;
                allocated.Add(entity.name, entity);
            }).WaitForCompletion();
            
            Debug.Log($"{allocated.Count} entities loaded");
        }
    }
}