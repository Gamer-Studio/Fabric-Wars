using System.Collections.Generic;
using FabricWars.Game.Elements;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace FabricWars.Game.Recipes
{
    public partial class Recipe
    {
        // Scoped recipe section
        public static readonly Dictionary<Element, List<(int scope, ScopeRecipe recipe)>> allocatedScopeRecipe = new();
        
        internal static void LoadScopeRecipe(IEnumerable<Element> loadedElements)
        {
            foreach (var element in loadedElements) allocatedScopeRecipe.Add(element, new List<(int scope, ScopeRecipe recipe)>());

            var recipes = new List<ScopeRecipe>();
            
            Addressables.LoadAssetsAsync<ScopeRecipe>(new AssetLabelReference { labelString = "ScopeRecipe" },
                recipe =>
                {
                    var noRequireElement = true;
                    foreach (var (element, scope) in recipe.scopes)
                    {
                        allocatedScopeRecipe[element].Add((scope, recipe));
                        noRequireElement = false;
                    }
                    
                    if (noRequireElement)
                    {
                        allocatedScopeRecipe[Element.None].Add((0, recipe));
                    }
                    
                    recipes.Add(recipe);
                }).WaitForCompletion();
            
            Debug.Log($"{recipes.Count} scope recipes loaded");
        }
    }
}