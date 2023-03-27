using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Entities
{
    public partial class Entity
    {
        private static bool _loaded = false;
        
        public readonly static Dictionary<string, GameObject> allocated = new ();
        
        public static void Load()
        {
            if(_loaded) return;
            
            Addressables.LoadAssetsAsync<GameObject>(new AssetLabelReference{labelString = "EntityPrefab"}, entity =>
            {
                if (allocated.ContainsKey(entity.name)) return;
                allocated.Add(entity.name, entity);
            });
            
            Debug.Log($"{allocated.Count} entities loaded");
            
            _loaded = true;
        }
    }
}