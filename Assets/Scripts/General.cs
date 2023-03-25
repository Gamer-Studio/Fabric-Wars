using System.Collections.Generic;
using FabricWars.Game.Elements;
using FabricWars.Game.Items;
using FabricWars.Game.Recipes;

namespace FabricWars
{
    public static class General
    {
        private static bool _inited;

        public static Dictionary<string, Element> elements => Element.allocated;
        public static Dictionary<string, Item> items => Item.allocated;
        public static Dictionary<Element, List<(int scope, ScopedRecipe recipe)>> recipeScopes => Recipe.allocatedScopedRecipe;
        
        public static void Initialization()
        {
            if (_inited) return;

            var loadedElements = Element.Load();
            Item.Load();
            Recipe.LoadScopedRecipe(loadedElements);
            
            
            _inited = true;
        }
    }
}