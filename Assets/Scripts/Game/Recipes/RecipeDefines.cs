using System.Collections.Generic;
using FabricWars.Game.Elements;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Recipes
{
    public partial class Recipe
    {
        // Scoped recipe section
        private static bool _scopedLoaded = false;
        
        public static readonly Dictionary<Element, List<(int scope, ScopedRecipe recipe)>> allocatedScopedRecipe = new();
        
        public static void LoadScopedRecipe(IEnumerable<Element> loadedElements)
        {
            if(_scopedLoaded) return;

            foreach (var element in loadedElements) allocatedScopedRecipe.Add(element, new List<(int scope, ScopedRecipe recipe)>());
            
            var recipes = new List<ScopedRecipe>();
            
            Addressables.LoadAssetsAsync<ScopedRecipe>(new AssetLabelReference { labelString = "ScopedRecipe" },
                recipe =>
                {
                    var noRequireElement = true;
                    foreach (var (element, scope) in recipe.scopes)
                    {
                        allocatedScopedRecipe[element].Add((scope, recipe));
                        noRequireElement = false;
                    }
                    
                    if (noRequireElement)
                    {
                        allocatedScopedRecipe[Element.None].Add((0, recipe));
                    }
                    
                    recipes.Add(recipe);
                }).WaitForCompletion();
            
            Debug.Log($"{recipes.Count} scoped recipes loaded");
            
            _scopedLoaded = true;
        }
    }
}