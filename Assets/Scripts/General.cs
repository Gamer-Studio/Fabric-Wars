using System.Collections.Generic;
using FabricWars.Game.Elements;
using FabricWars.Game.Items;
using FabricWars.Game.Recipes;

namespace FabricWars
{
    public static class General
    {
#if UNITY_EDITOR
        private static bool _inited;
#endif
        public static Dictionary<string, Element> elements => Element.allocated;
        public static Dictionary<string, Item> items => Item.allocated;

        public static Dictionary<Element, List<(int scope, ScopedRecipe recipe)>> recipeScopes =>
            Recipe.allocatedScopedRecipe;

        public static void Initialization()
        {
#if UNITY_EDITOR
            if (_inited) return;
            _inited = true;
#endif

            var loadedElements = Element.Load();
            Item.Load();
            Recipe.LoadScopedRecipe(loadedElements);
        }
    }
}