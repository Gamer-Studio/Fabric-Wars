using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Elements
{
    public partial class Element
    {
        
        public static readonly Dictionary<string, Element> allocated = new();

        public static Element None, Karma, Life, Water, Fire, Gold;

        internal static IEnumerable<Element> Load()
        {
            // Loading addressable elements data
            var loadedData = new List<Element>();
            
            Addressables.LoadAssetsAsync<Element>(new AssetLabelReference { labelString = "Element" },
                element =>
                {
                    allocated[element.name] = element;
                    loadedData.Add(element);
                }).WaitForCompletion();
            
            // Allocate elements
            allocated.TryGetValue("None", out None);
            allocated.TryGetValue("Karma", out Karma);
            allocated.TryGetValue("Life", out Life);
            allocated.TryGetValue("Water", out Water);
            allocated.TryGetValue("Fire", out Fire);
            allocated.TryGetValue("Gold", out Gold);
            
            Debug.Log($"{loadedData.Count} elements loaded");
            
            return loadedData;
        }
    }
}