using System.Collections.Generic;
using System.Linq;
using FabricWars.Game.Items;
using FabricWars.Game.Recipes;
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

        public static readonly Dictionary<Element, List<(int scope, ScopedRecipe recipe)>> recipeScopes = new();
        
        public static void Initialization()
        {
            if (inited) return;

            Addressables.LoadAssetsAsync<Element>(new AssetLabelReference { labelString = "ElementSO" },
                element =>
                {
                    elements[element.name] = element;
                    recipeScopes[element] = new List<(int, ScopedRecipe)>();
                }).WaitForCompletion();
            
            Debug.Log($"Loaded {elements.Count} elements");

            Element.Init();
            
            Addressables.LoadAssetsAsync<Item>(new AssetLabelReference { labelString = "ItemSO" },
                item => items[item.name] = item).WaitForCompletion();

            Debug.Log($"Loaded {items.Count} items");
            
            Item.Init();

            Addressables.LoadAssetsAsync<ScopedRecipe>(new AssetLabelReference { labelString = "ScopedRecipe" },
                recipe =>
                {
                    var noRequireElement = true;
                    foreach (var (element, scope) in recipe.scopes)
                    {
                        recipeScopes[element].Add((scope, recipe));
                        noRequireElement = false;
                    }
                    
                    if (noRequireElement)
                    {
                        recipeScopes[Element.None].Add((0, recipe));
                    }
                }).WaitForCompletion();
            
            Debug.Log($"Loaded {recipeScopes.Count} scoped recipes");
            
            inited = true;
        }
    }
}