using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Elements
{
    public partial class Element
    {
        private static bool _loaded = false;
        
        public static readonly Dictionary<string, Element> allocated = new();

        public static Element None, Causality, Life, Water, Fire, Gold;

        public static IEnumerable<Element> Load()
        {
            if (_loaded) return null;
            
            // Loading addressable elements data
            var loadedData = new List<Element>();
            
            Addressables.LoadAssetsAsync<Element>(new AssetLabelReference { labelString = "ElementSO" },
                element =>
                {
                    allocated[element.name] = element;
                    loadedData.Add(element);
                }).WaitForCompletion();
            
            // Allocate elements
            allocated.TryGetValue("None", out None);
            allocated.TryGetValue("Causality", out Causality);
            allocated.TryGetValue("Life", out Life);
            allocated.TryGetValue("Water", out Water);
            allocated.TryGetValue("Fire", out Fire);
            allocated.TryGetValue("Gold", out Gold);
            
            Debug.Log($"{loadedData.Count} elements loaded");
            _loaded = true;
            
            return loadedData;
        }
    }
}